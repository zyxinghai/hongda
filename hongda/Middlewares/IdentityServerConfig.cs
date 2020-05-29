using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hongda.Middlewares
{
    /// <summary>
    /// ids4
    /// </summary>
    public class IdentityServerConfig
    {
        /// <summary>	
        /// 需要保护的api资源	
        /// </summary>	
        public static IEnumerable<ApiResource> Apis =>
            new List<ApiResource>
            {
                new ApiResource("api1","My Api")
            };

        /// <summary>
        /// 
        /// </summary>
        public static IEnumerable<Client> Clients =>
            new List<Client>
            {	
                //客户端	
                new Client
                {
                    ClientId="client",
                    ClientSecrets={ new Secret("aju".Sha256())},
                    AllowedGrantTypes=GrantTypes.ResourceOwnerPassword,	
                    //如果要获取refresh_tokens ,必须在scopes中加上OfflineAccess	
                    AllowedScopes={ "api1", IdentityServerConstants.StandardScopes.OfflineAccess},
                    AllowOfflineAccess=true
                }
            };

        /// <summary>
        /// 
        /// </summary>
        public static List<TestUser> Users = new List<TestUser>
        {
            new TestUser
            {
                SubjectId="001",
                Password="Aju_001",
                Username="Aju_001"
            },
            new TestUser
            {
                 SubjectId="002",
                Password="Aju_002",
                Username="Aju_002"
            }
        };
    }
}
