using System;
using System.Collections.Generic;
using System.Security.Claims;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using Microsoft.Extensions.Configuration;

namespace AuthorizationService
{
    public class Config
    {
        private static IConfigurationRoot Configuration { get; set; }

        public static void InitializeConfiguration(IConfigurationRoot configuration)
        {
            Configuration = configuration;
        }
        // scopes define the resources in your system
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResource("ServiceOrderMvc.roles",new []{ "serviceProvider", "admin" , "client" } )
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource
                {
                    Name =  Configuration["localization_api:name"],
                    DisplayName =  Configuration["localization_api:display_name"],
                    UserClaims = {"email"},
                    Scopes =
                    {
                        new Scope
                        {
                            Name = "localizationScope.admin",
                            DisplayName = "Admin access to localization API resource"
                        },
                        new Scope
                        {
                            Name = "localizationScope.owner",
                            DisplayName = "Owner of resources in localization API resource"
                            
                        },
                        new Scope
                        {
                            Name = "localizationScope.readOnly",
                            DisplayName = "Only read access in localization API resource"
                        }
                    }
                }
            };
        }

        // clients want to access resources (aka scopes)
        public static IEnumerable<Client> GetClients()
        {
            // client credentials client
            return new List<Client>
            {

                // OpenID Connect hybrid flow and client credentials client (MVC)
                new Client
                {
                    ClientId =  Configuration["service_order_client:client_id"],
                    ClientName = Configuration["service_order_client:client_name"],
                    AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,

                    RequireConsent = false,

                    ClientSecrets =
                    {
                        new Secret(Configuration["service_order_client:client_secrets"].Sha256())
                    },

                    RedirectUris = {Configuration["service_order_client:redirect_uri"] },
                    PostLogoutRedirectUris = { Configuration["service_order_client:post_logout_redirect_uris"] },
                    ClientUri = Configuration["service_order_client:client_uri"],

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "localizationScope.readOnly",
                        "localizationScope.owner",
                        "ServiceOrderMvc.roles"
                    },
                    AlwaysIncludeUserClaimsInIdToken = true,
                    AllowOfflineAccess = true
                }
            };
        }
    }
}
