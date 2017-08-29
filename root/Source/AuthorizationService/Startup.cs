using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using IdentityServer4.EntityFramework.DbContexts;
using System.Linq;
using IdentityServer4.EntityFramework.Mappers;

using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Security.Claims;
using AuthorizationService.Data;
using AuthorizationService.IdentityServer;
using AuthorizationService.Models.UserData;
using AuthorizationService.Services;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AuthorizationService
{
    public class Startup
    {

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see https://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets<Startup>();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
            Config.InitializeConfiguration(Configuration);
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            // Add framework services.
            services.AddDbContext<AuthorizationServiceDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddIdentity<AuthorizationServiceUser, IdentityRole>()
                .AddEntityFrameworkStores<AuthorizationServiceDbContext>()
                .AddDefaultTokenProviders();
                
           

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
            services.AddTransient<IProfileService, IdentityProfileService>();


            services.AddIdentityServer()
                .AddTemporarySigningCredential()
                .AddConfigurationStore(builder =>
                    builder.UseSqlServer(connectionString, options =>
                        options.MigrationsAssembly(migrationsAssembly)))
                .AddOperationalStore(builder =>
                    builder.UseSqlServer(connectionString, options =>
                        options.MigrationsAssembly(migrationsAssembly)))
                .AddAspNetIdentity<AuthorizationServiceUser>()
                .AddProfileService<IdentityProfileService>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            });
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            InitializeDatabase(app);

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }         

            app.UseIdentity();
            app.UseIdentityServer();

            // Add external authentication middleware below. To configure them please see https://go.microsoft.com/fwlink/?LinkID=532715
            app.UseGoogleAuthentication(new GoogleOptions
            {
                AuthenticationScheme = "Google",
                SignInScheme = "Identity.External", // this is the name of the cookie middleware registered by UseIdentity()
                ClientId = Configuration["web_google:client_id"],
                ClientSecret = Configuration["web_google:client_secret"],
            });

            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }

        private async void InitializeDatabase(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                scope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();
                scope.ServiceProvider.GetRequiredService<AuthorizationServiceDbContext>().Database.Migrate();
                
                var context = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                context.Database.Migrate();
                if (!context.Clients.Any())
                {
                    foreach (var client in Config.GetClients())
                    {
                        context.Clients.Add(client.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.IdentityResources.Any())
                {
                    foreach (var resource in Config.GetIdentityResources())
                    {
                        context.IdentityResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.ApiResources.Any())
                {
                    foreach (var resource in Config.GetApiResources())
                    {
                        context.ApiResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }

                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var clients = Config.GetClients();
                foreach (var client in clients)
                {
                    var clientScopeName = client.ClientId + ".roles";
                    if (client.AllowedScopes.Any(source => source == clientScopeName))
                    {
                        var clientClaims = context.IdentityResources.FirstOrDefault(src => src.Name == clientScopeName)?.UserClaims;
                        if (clientClaims != null)
                        {
                            foreach (var claim in clientClaims)
                            {
                                if (!await roleManager.RoleExistsAsync(claim.Type))
                                {
                                    var role = new IdentityRole(claim.Type);
                                    await roleManager.CreateAsync(role);
                                    await roleManager.AddClaimAsync(role, new Claim("clientScope",clientScopeName));
                                }
                                    
                            }
                        }
                    }             
                }
                if(!await roleManager.RoleExistsAsync(Configuration["authorize_service:admin_role"]))
                {
                    var adminRole = new IdentityRole(Configuration["authorize_service:admin_role"]);
                    await roleManager.CreateAsync(adminRole);
                    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AuthorizationServiceUser>>();
                    var adminUser = new AuthorizationServiceUser
                    {
                        Email = Configuration["authorize_service:admin:email"],
                        UserName = Configuration["authorize_service:admin:email"]
                    };
                    await userManager.CreateAsync(adminUser, Configuration["authorize_service:admin:password"]);
                    await userManager.AddToRoleAsync(adminUser, adminRole.Name);
                }


            }
            
        }
    }

}
