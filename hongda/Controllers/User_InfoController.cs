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
    /// 用户
    /// </summary>
    [Route("api/[Controller]")]
    [ApiController]
    public class User_InfoController : ControllerBase
    {
        /// <summary>
        /// 用户查询
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost("ALL")]
        public object ALL(Dm_Paging value)
        {
            try
            {

                Dm_ResultList result = User_InfoBLO.Query(value);

                return result;

            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}
