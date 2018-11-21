using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer4.Study.Server
{
    public class Config
    {
        /// <summary>
        /// 定义api
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("api1", "My API")
            };
        }
        /// <summary>
        /// 定义一个客户端
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "client",

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    // scopes that client has access to
                    AllowedScopes = { "api1" }
                },
                 new Client
                {
                    ClientId = "ro.client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = { "api1" }
                },
                  new Client
                    {
                        ClientId = "mvc",
                        ClientName = "MVC Client",
                        //AllowedGrantTypes = GrantTypes.Implicit,
                        AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,
                         ClientSecrets =
                        {
                            new Secret("secret".Sha256())
                        },
                        // where to redirect to after login
                        RedirectUris = { "https://localhost:5002/signin-oidc" },

                        // where to redirect to after logout
                        PostLogoutRedirectUris = { "https://localhost:5002/signout-callback-oidc" },

                        AllowedScopes = new List<string>
                        {
                            IdentityServerConstants.StandardScopes.OpenId,
                            IdentityServerConstants.StandardScopes.Profile,
                            "api1"
                        },
                         AllowOfflineAccess = true
                    },
                  new Client
                    {
                        ClientId = "js",
                        ClientName = "JavaScript Client",
                        AllowedGrantTypes = GrantTypes.Implicit,
                        AllowAccessTokensViaBrowser = true,

                        RedirectUris =           { "http://localhost:5003/account/index" },
                        PostLogoutRedirectUris = { "http://localhost:5003/Home/index" },
                        AllowedCorsOrigins =     { "http://localhost:5003" },

                        AllowedScopes =
                        {
                            IdentityServerConstants.StandardScopes.OpenId,
                            IdentityServerConstants.StandardScopes.Profile,
                            "api1"
                        }
                    }
            };
        }
        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
                    {
                        new TestUser
                        {
                            SubjectId = "1",
                            Username = "alice",
                            Password = "password"
                        },
                        new TestUser
                        {
                            SubjectId = "2",
                            Username = "bob",
                            Password = "password"
                        }
                    };
        }
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
                    {
                        new IdentityResources.OpenId(),
                        new IdentityResources.Profile(),
                    };
        }
    }
}
