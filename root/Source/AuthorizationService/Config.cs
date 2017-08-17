﻿using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;

namespace AuthorizationService
{
    public class Config
    {
        // scopes define the resources in your system
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResource("mvcScope",new []{ "role", "admin", "serviceProvider","client"} )
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource
                {
                    Name = "localization",
                    DisplayName = "Localization API",
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
                            DisplayName = "Owner of resources in localization API resource",
                            UserClaims = {"role"}
                            
                        },
                        new Scope
                        {
                            Name = "localizationScope.readOnly",
                            DisplayName = "Only read access in localization API resource",
                            UserClaims = {"role"}
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
                    ClientId = "mvc",
                    ClientName = "MVC Client",
                    AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,

                    RequireConsent = true,

                    ClientSecrets =
                    {
                        new Secret("mvc secret".Sha256())
                    },

                    RedirectUris = { "http://localhost:5002/signin-oidc" },
                    PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "localizationScope.readOnly",
                        "localizationScope.owner",
                        "mvcScope"
                    },
                    AllowOfflineAccess = true
                }
            };
        }
    }
}