using IdentityServer4;
using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace API.IdentityServer
{
    public static class IdentityServerConfig
    {

        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };

        public static IEnumerable<ApiScope> ApiScopes =>
             new ApiScope[]
             {
                  new ApiScope("rookie.api", "Rookie API")
             };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                // machine to machine client
                new Client
                {
                    ClientId = "client",
                    ClientSecrets = { new Secret("secret".Sha256()) },

                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    // scopes that client has access to
                    AllowedScopes = { "rookie.api" }
                },

                // interactive ASP.NET Core MVC client
                new Client
                {
                    ClientId = "mvc",
                    ClientSecrets = { new Secret("secret".Sha256()) },

                    AllowedGrantTypes = GrantTypes.Code,

                    RedirectUris = { "https://tdatcustomer.azurewebsites.net/signin-oidc" },

                    PostLogoutRedirectUris = { "https://tdatcustomer.azurewebsites.net/signout-callback-oidc" },

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "rookie.api"
                    }
                },
                new Client
                {
                    ClientId = "react",                  
                    AllowedGrantTypes = GrantTypes.Implicit,
                    RedirectUris = { "https://tdatadmin.z23.web.core.windows.net/signin-oidc" },
                    PostLogoutRedirectUris = { "https://tdatadmin.z23.web.core.windows.net/signout-oidc" },
                    AllowedCorsOrigins={"https://tdatadmin.z23.web.core.windows.net"},
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "rookie.api"
                    },
                    AllowAccessTokensViaBrowser=true,
                    RequireConsent=false,
                },

                new Client
                {
                    ClientId = "swagger",
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Code,

                    RequireConsent = false,
                    RequirePkce = true,

                    RedirectUris =           { $"https://aoishin.azurewebsites.net/swagger/oauth2-redirect.html" },
                    PostLogoutRedirectUris = { $"https://aoishin.azurewebsites.net/swagger/oauth2-redirect.html" },
                    AllowedCorsOrigins =     { $"https://aoishin.azurewebsites.net" },

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "rookie.api"
                    }
                },
            };
    }
}