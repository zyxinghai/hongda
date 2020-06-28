using hongda.Entity;
using hongda.ResultEntities;
using hongda.Services;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hongda.Middlewares
{
    /// <summary>
    /// ids4自定义
    /// </summary>
    public class ResourceOwnerValidator : IResourceOwnerPasswordValidator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            User_Login ul = new User_Login();
            ul.Login_Name = context.UserName;
            ul.Login_PassWord = context.Password;
            ul.Login_Ip = "";
            Dm_ResultDefault dm= User_LoginBLO.Logins(ul);
            //if (context.UserName == "Aju" && context.Password == "Aju_password")
            if (dm.Result==1)
            {
                context.Result = new GrantValidationResult(
                    subject: context.UserName,
                    authenticationMethod: OidcConstants.AuthenticationMethods.Password);
            }
            else
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "无效的秘钥");
            }
            return Task.FromResult("");
        }

    }
}
