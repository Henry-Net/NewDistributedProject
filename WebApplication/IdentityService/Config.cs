using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityService
{
    public class Config
    {

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    //客户端的唯一ID
                    ClientId = "client",
                    // 客户端机密列表 - 访问令牌端点的凭据。
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    //刷新令牌时，将刷新刷新令牌的生命周期（按SlidingRefreshTokenLifetime中指定的数量）。生命周期不会超AbsoluteRefreshTokenLifetime。
                    RefreshTokenExpiration = TokenExpiration.Sliding,
                    //指定此客户端是否可以请求刷新令牌（请求offline_access范围）
                    AllowOfflineAccess = true,
                    //指定此客户端是否需要密钥才能从令牌端点请求令牌（默认为true）
                    RequireClientSecret = false,
                    //指定允许客户端使用的授权类型。使用GrantTypes该类进行常见组合。
                    AllowedGrantTypes = new List<string>{ "sms_auth_code" },

                    //在请求id令牌和访问令牌时，如果用户声明始终将其添加到id令牌而不是请求客户端使用userinfo端点。默认值为false。
                    AlwaysIncludeUserClaimsInIdToken = true,

                    //默认情况下，客户端无权访问任何资源 - 通过添加相应的范围名称来指定允许的资源
                    AllowedScopes = new List<string>{
                        "user_api",
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,

                    }
                }
            };
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            return new List<ApiResource>
            {
                new ApiResource("user_api", "User API service")
            };
        }

    }
}
