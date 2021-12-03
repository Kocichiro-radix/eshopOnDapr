using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;

namespace Microsoft.eShopOnDapr.Services.Identity.API
{
    public class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("basket", "Access to Basket API"),
                new ApiScope("ordering", "Access to Ordering API"),
                new ApiScope("shoppingaggr", "Access to Shopping Aggregator API"),
                //scope名-Disp名
                new ApiScope("testms", "Access to TestMS API"),
            };
    
        public static IEnumerable<ApiResource> ApiResources =>
            new ApiResource[]
            {
                new ApiResource("basket-api", "Basket API")
                {
                    Scopes = { "basket" }

                },
                new ApiResource("ordering-api", "Ordering API")
                {
                    Scopes = { "ordering" }
                },
                new ApiResource("shoppingaggr-api", "Shopping Aggregator API")
                {
                    Scopes = { "shoppingaggr" }
                },
                //API名-Disp名
                new ApiResource("testmsapi", "TestMS API")
                {
                    //scope名
                    Scopes = { "testms" }
                }
            };

        public static IEnumerable<Client> GetClients(IConfiguration configuration)
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "blazor",
                    ClientName = "Blazor Front-end",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,

                    RequireConsent = false,

                    AllowedCorsOrigins = { configuration["BlazorClientUrlExternal"] },

                    // where to redirect to after login
                    RedirectUris = { $"{configuration["BlazorClientUrlExternal"]}/authentication/login-callback" },

                    // where to redirect to after logout
                    PostLogoutRedirectUris =
                    {
                        $"{configuration["BlazorClientUrlExternal"]}/authentication/logout-callback"
                    },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "basket",
                        "ordering",
                        "shoppingaggr",
                        "testms"
                    },
                },
                new Client
                {
                    ClientId = "basketswaggerui",
                    ClientName = "Basket Swagger UI",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris = { $"{configuration["BasketApiUrlExternal"]}/swagger/oauth2-redirect.html" },
                    PostLogoutRedirectUris = { $"{configuration["BasketApiUrlExternal"]}/swagger/" },

                    AllowedScopes =
                    {
                        "basket"
                    }
                },
                new Client
                {
                    ClientId = "orderingswaggerui",
                    ClientName = "Ordering Swagger UI",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris = { $"{configuration["OrderingApiUrlExternal"]}/swagger/oauth2-redirect.html" },
                    PostLogoutRedirectUris = { $"{configuration["OrderingApiUrlExternal"]}/swagger/" },

                    AllowedScopes =
                    {
                        "ordering"
                    }
                },
                new Client
                {
                    ClientId = "shoppingaggrswaggerui",
                    ClientName = "Shopping Aggregator Swagger UI",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris = { $"{configuration["ShoppingAggregatorApiUrlExternal"]}/swagger/oauth2-redirect.html" },
                    PostLogoutRedirectUris = { $"{configuration["ShoppingAggregatorApiUrlExternal"]}/swagger/" },

                    AllowedScopes =
                    {
                        "basket",
                        "shoppingaggr"
                    }
                },
                new Client
                {
                    //id
                    ClientId = "testmsswaggerui",
                    //表示名
                    ClientName = "TestMS Swagger UI",
                    //暗黙 更新トークン等の高度な機能は無
                    AllowedGrantTypes = GrantTypes.Implicit,
                    //クライアントがブラウザを介してアクセストークンを受信できるようにするかどうか
                    AllowAccessTokensViaBrowser = true,
                    //トークンまたは認証コードを返すことができるURI
                    RedirectUris = { $"{configuration["TestMSApiUrlExternal"]}/swagger/oauth2-redirect.html" },
                    //ログアウト後にリダイレクトできるURI
                    PostLogoutRedirectUris = { $"{configuration["TestMSApiUrlExternal"]}/swagger/" },
                    //対応するスコープ名を追加して、許可されるリソースを指定
                    AllowedScopes =
                    {
                        "testms"
                    }
                }
            };
        }
    }
}