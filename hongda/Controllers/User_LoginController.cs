using hongda.Entity;
using hongda.ResultEntities;
using hongda.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hongda.Controllers
{
    /// <summary>
    /// 登录
    /// </summary>
    [Route("api/[Controller]")]
    [ApiController]
    [Authorize]
    public class User_LoginController : ControllerBase
    {
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost("Login")]
        public object Login(User_Login value)
        {
            try
            {
                //执行业务操作
                Dm_ResultDefault result = User_LoginBLO.Login(value);

                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
