using hongda.ResultEntities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hongda.Middlewares
{
    /// <summary>
    /// 异常
    /// </summary>
    public class ValidateModelAttribute : ExceptionFilterAttribute
    {
        /// <summary>
        /// 异常拦截
        /// </summary>
        /// <param name="actionExecutedContext"></param>
        public override void OnException(ExceptionContext actionExecutedContext)
        {
            base.OnException(actionExecutedContext);

            string errMsg = actionExecutedContext.Exception.Message;//异常错误信息

            string requestUrl = actionExecutedContext.HttpContext.Request.Path;//请求的接口地址

            Dm_ResultDefault result = new Dm_ResultDefault() { Result = 0, ResultDesc = errMsg };//响应内容
            //dm_ResultDefault result = new dm_ResultDefault() { Result = 0, ResultDesc = "网络异常，请重新操作" };//响应内容

            string serialResult = JsonConvert.SerializeObject(result);//序列化的响应内容

            //日志返回记录
            /*Loginterface_Entity le = new Loginterface_Entity();
            le.logIfName = requestUrl;
            le.logIfParameter = "[异常] " + serialResult;
            le.logIfType = 0;
            le.logIfSource = 3;
            le.logTime = DateTime.Now;
            if (LoginterfaceDAO.Add(le) == 0)
            {
                actionExecutedContext.Result = new OkObjectResult(new BaseResultModel(code: 200, result: "", message: "日志记录错误"));
            }*/

            actionExecutedContext.Result = new OkObjectResult(new BaseResultModel(code: 200, result: serialResult, message: "错误信息"));
        }
    }
}
