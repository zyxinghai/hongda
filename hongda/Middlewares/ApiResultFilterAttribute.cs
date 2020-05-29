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
    /// 拦截
    /// </summary>
    public class ApiResultFilterAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 进行token验证
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            base.OnActionExecuting(actionContext);

            if (actionContext.HttpContext.Request.Method == "POST")
            {

                //没有入参则不进行后续解密，TOKEN验证和日志记录等，如Upload接口
                if (!actionContext.ActionArguments.Keys.Contains("value")) return;

                string requestUrl = actionContext.HttpContext.Request.Path;//请求的接口地址
                //string requestData = actionContext.ActionArguments["value"].ToString();//获取请求数据内容
                string requestData = JsonConvert.SerializeObject(actionContext.ActionArguments["value"]);
                if (requestUrl != null && requestUrl != "")
                {

                    UserBaseInfo ubi = new UserBaseInfo();
                    ubi.UserId = Guid.NewGuid();

                    HttpSession.SetSession("UserBaseInfo", JsonConvert.SerializeObject(ubi));
                    //缓存
                    /*if (requestUrl == "/api/ConfigParameter/ALL")
                    {
                        if (MemoryCacheHelper.Exists(requestData))
                        {
                            var cach = MemoryCacheHelper.Get(requestData).ToString();
                            actionContext.Result = new OkObjectResult(new BaseResultModel(code: 200, result: cach, message: "缓存获取成功"));
                        }
                    }*/


                    /* api_limit_entity aie = new api_limit_entity();
                     aie.apilimit_api = requestUrl;
                     Dm_ResultDefault yzapi = userloginBLO.api(aie);
                     if (yzapi.Result == 0)
                     {

                         string token = actionContext.HttpContext.Request.Headers["token"];
                         //token = "8832b602-220c-11ea-ae54-00163e09749d";
                         string user_id = "";
                         if (token == null || token == "" || token == "null")
                         {
                             actionContext.Result = new OkObjectResult(new BaseResultModel(code: 200, result: "", message: "身份未验证通过"));
                         }
                         else
                         {
                             check_token_entity cte = new check_token_entity();
                             cte.cktoken_token = new Guid(token);
                             Dm_ResultDefault yz = userloginBLO.token(cte);
                             if (yz.Result == 0)
                             {
                                 actionContext.Result = new OkObjectResult(new BaseResultModel(code: 200, result: "", message: "身份未验证通过"));
                             }
                             user_id = "" + yz.ResultDesc + "";
                         }

                         requestData = requestData.Replace("}", ",\"" + "only_user\":\"" + user_id + "\"}");
                         actionContext.ActionArguments["value"] = requestData;
                     }*/
                }
                else
                {
                    actionContext.Result = new OkObjectResult(new BaseResultModel(code: 200, result: "", message: "未接收到请求"));
                }
                //string plainData = requestData;//非密文无需解密
                //logger.InsertLog(requestUrl, "[请求] " + plainData, 1, "", 1, "", "", "", "");//记录接口请求日志
               /* Loginterface_Entity le = new Loginterface_Entity();
                le.logIfName = requestUrl;
                le.logIfParameter = "[请求] " + requestData;
                le.logIfType = 1;
                le.logIfSource = 1;
                le.logTime = DateTime.Now;
                if (LoginterfaceDAO.Add(le) == 0)
                {
                    actionContext.Result = new OkObjectResult(new BaseResultModel(code: 200, result: "", message: "日志记录错误"));
                }*/




                //if (!requestData.StartsWith("{") && !requestData.StartsWith("<"))//请求数据为JSON或XML明文则无需解密，如微信端公众号或支付推送报文等
                //{
                //    //请求数据解密
                //    requestData = requestData.Replace("\"", "");//替换两边的引号字符

                //    actionContext.ActionArguments["value"] = plainData+user_id;//传给Action中明文参数
                //}

            }

        }


        /// <summary>
        /// 处理正常返回的结果对象，进行统一json格式包装
        /// 返回响应记录日志
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Result != null)
            {
                var result = context.Result as ObjectResult;
                string requestUrl = context.HttpContext.Request.Path;//请求的接口地址

                JsonResult newresult;
                if (context.Result is ObjectResult)
                {
                    newresult = new JsonResult(new { code = 200, body = result.Value });
                }
                else if (context.Result is EmptyResult)
                {
                    newresult = new JsonResult(new { code = 200, body = new { } });
                }
                else
                {
                    throw new Exception($"未经处理的Result类型：{ context.Result.GetType().Name}");
                }

                string serialResult = JsonConvert.SerializeObject(result.Value);//序列化的响应内容
                                                                                //缓存
                /*var cach = "";
                if (!MemoryCacheHelper.Exists(requestUrl))
                {
                    cach = MemoryCacheHelper.Set(requestUrl,serialResult).ToString();
                }*/


                //日志返回记录
               /* Loginterface_Entity le = new Loginterface_Entity();
                le.logIfName = requestUrl;
                le.logIfParameter = "[响应] " + serialResult;
                le.logIfType = 1;
                le.logIfSource = 2;
                le.logTime = DateTime.Now;
                if (LoginterfaceDAO.Add(le) == 0)
                {
                    context.Result = new OkObjectResult(new BaseResultModel(code: 200, result: "", message: "日志记录错误"));
                }*/

                context.Result = new OkObjectResult(new BaseResultModel(code: 200, result: serialResult, message: "通过"));
            }
            base.OnActionExecuted(context);
        }

    }
}
