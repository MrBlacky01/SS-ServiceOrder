using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace ServiceOrder.MvcClientCore
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.


            services.AddAuthentication(options =>
                {
                    options.DefaultScheme = "Cookies";
                    options.DefaultChallengeScheme = "oidc";
                })
                .AddCookie("Cookies")
                .AddOpenIdConnect("oidc", options =>
                {
                    options.ClientId = "ServiceOrderMvc";
                    options.ClientSecret = "mvc secret";
                    options.SignInScheme = "Cookies";
                    options.Authority = "http://localhost:5000";
                    options.RequireHttpsMetadata = false;
                    options.ResponseType = "code id_token";
                    options.Scope.Add("localizationScope.owner");
                    options.Scope.Add("offline_access");
                    options.Scope.Add("ServiceOrderMvc.roles");
                    options.Scope.Add("localizationScope.readOnly");
                    options.Scope.Add("email");
                    options.GetClaimsFromUserInfoEndpoint = true;
                    options.SaveTokens = true;

                });


            services.AddAuthorization(option =>
            {
                option.AddPolicy("Admin", policy => policy.RequireClaim("role", "admin").RequireClaim("clientScope", "ServiceOrderMvc.roles"));
                option.AddPolicy("ServiceProvider", policy => policy.RequireClaim("role","serviceProvider").RequireClaim("clientScope", "ServiceOrderMvc.roles"));
                option.AddPolicy("Client", policy => policy.RequireClaim("role", "client").RequireClaim("clientScope", "ServiceOrderMvc.roles"));
            });

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseAuthentication();
            /*app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationScheme = "Cookies"
            });

            app.UseOpenIdConnectAuthentication(new OpenIdConnectOptions
            {
                AuthenticationScheme = "oidc",
                SignInScheme = "Cookies",

                Authority = "http://localhost:5000",
                RequireHttpsMetadata = false,

                ClientId = "ServiceOrderMvc",
                ClientSecret = "mvc secret",

                ResponseType = "code id_token",
                Scope = { "localizationScope.owner", "offline_access", "ServiceOrderMvc.roles", "localizationScope.readOnly", "email" },

                GetClaimsFromUserInfoEndpoint = true,
                SaveTokens = true,
                
            });*/

            app.UseStatusCodePagesWithReExecute("/home/errorstatus/{0}.html");
            app.UseStaticFiles();


            app.UseMvcWithDefaultRoute();
        }
    }
}
