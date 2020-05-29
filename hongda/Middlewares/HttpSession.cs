using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hongda.Middlewares
{
    /// <summary>
    /// 
    /// </summary>
    public class HttpSession
    {

        /// <summary>
        /// 设置Session
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public static void SetSession(string key, string value)
        {

            _accessor.HttpContext.Session.SetString(key, value);
        }

        /// <summary>
        /// 获取Session
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static UserBaseInfo GetSession(string key)
        {
            var value = _accessor.HttpContext.Session.GetString(key);
            //if (string.IsNullOrEmpty(value))
            //    value = string.Empty;
            return JsonConvert.DeserializeObject<UserBaseInfo>(value);
        }
        private static IHttpContextAccessor _accessor;
        //public static Microsoft.AspNetCore.Http.HttpContext Current => _accessor.HttpContext;
        internal static void Configure(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }
    }
}
