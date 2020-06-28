using Dapper;
using hongda.DataAccess;
using hongda.Entity;
using hongda.Middlewares;
using hongda.ResultEntities;
using IdentityModel.Client;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace hongda.Services
{
    /// <summary>
    /// 用户登录
    /// </summary>
    public class User_LoginBLO
    {
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Dm_ResultDefault Login(User_Login obj)
        {
            try
            {
                using (IDbConnection conn = new MySqlConnection(DapperHelper.CONNECTIONSTRING))
                {
                    //session取用户id
                    UserBaseInfo ubi = HttpSession.GetSession("UserBaseInfo");
                    conn.Open();
                    IDbTransaction tran = conn.BeginTransaction();
                    try
                    {
                        User_Info_Entity user_info = new User_Info_Entity();
                        var dynamicParams = new DynamicParameters();
                        List<User_Info_Entity> num=new List<User_Info_Entity>();
                        int name = 0;
                        dynamicParams.Add("login_password", obj.Login_PassWord);

                        if (Regex.IsMatch(obj.Login_Name, @"^1[3456789]\d{9}$"))
                        {
                            user_info.user_mobile = obj.Login_Name;
                            dynamicParams.Add("user_mobile", obj.Login_Name);
                            name = conn.RecordCount<User_Info_Entity>("where user_mobile=@user_mobile and user_state=1 ", dynamicParams);
                            if (name == 0)
                            {
                                return new Dm_ResultDefault() { Result = 0, ResultDesc = "账号错误！" };
                            }
                            num = conn.GetList<User_Info_Entity>("where user_mobile=@user_mobile and login_password=@login_password and user_state=1 ", dynamicParams).ToList();
                            if (num == null)
                            {
                                return new Dm_ResultDefault() { Result = 0, ResultDesc = "密码错误！" };
                            }
                        }
                        else
                        {
                            user_info.user_nickname = obj.Login_Name;
                            dynamicParams.Add("user_nickname", obj.Login_Name);
                            name = conn.RecordCount<User_Info_Entity>("where user_nickname=@user_nickname and user_state=1 ", dynamicParams);
                            if (name == 0)
                            {
                                return new Dm_ResultDefault() { Result = 0, ResultDesc = "账号错误！" };
                            }
                            num = conn.GetList<User_Info_Entity>("where user_nickname=@user_nickname and login_password=@login_password and user_state=1 ", dynamicParams).ToList();
                            if (num == null)
                            {
                                return new Dm_ResultDefault() { Result = 0, ResultDesc = "密码错误！" };
                            }
                        }

                        Check_Token_Entity cte = new Check_Token_Entity();
                        cte.cktoken_id = Guid.NewGuid();
                        cte.cktoken_ip = obj.Login_Ip;
                        cte.cktoken_ur_id = num[0].user_id;
                        cte.cktoken_time = DateTime.Now;
                        cte.cktoken_token = Guid.NewGuid().ToString();
                        cte.cktoken_state = 1;
                        User_LoginDAO.TokenAdd(conn,tran,cte);

                        if (num[0].login_firsttime==new DateTime())
                        {
                            User_Info_Entity uie = new User_Info_Entity();
                            uie.user_id = num[0].user_id;
                            uie.login_firsttime = DateTime.Now;
                            uie.login_ip = obj.Login_Ip;
                            uie.up_user = ubi.UserId;
                            uie.up_date = DateTime.Now;

                            if (User_LoginDAO.uieUpdate(conn, tran, uie)==0)
                            {
                                return new Dm_ResultDefault() { Result = 0, ResultDesc = "修改信息失败" };
                            }
                        }
                        else
                        {
                            User_Info_Entity uie = new User_Info_Entity();
                            uie.user_id = num[0].user_id;
                            uie.login_lasttime = DateTime.Now;
                            uie.login_ip = obj.Login_Ip;
                            uie.up_user = ubi.UserId;
                            uie.up_date = DateTime.Now;

                            if (User_LoginDAO.uieUpdates(conn, tran, uie) == 0)
                            {
                                return new Dm_ResultDefault() { Result = 0, ResultDesc = "修改信息失败" };
                            }
                        }

                        // Dm_ResultDefault a=Token();
                        tran.Commit();
                        Task<Dm_ResultDefault> a = Token(obj);

                        return new Dm_ResultDefault() { Result = 1, ResultDesc = "" };
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        throw ex;
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Dm_ResultDefault Logins(User_Login obj)
        {
            try
            {
                using (IDbConnection conn = new MySqlConnection(DapperHelper.CONNECTIONSTRING))
                {
                    //session取用户id
                    //UserBaseInfo ubi = HttpSession.GetSession("UserBaseInfo");
                    conn.Open();
                    IDbTransaction tran = conn.BeginTransaction();
                    try
                    {
                        User_Info_Entity user_info = new User_Info_Entity();
                        var dynamicParams = new DynamicParameters();
                        List<User_Info_Entity> num = new List<User_Info_Entity>();
                        int name = 0;
                        dynamicParams.Add("login_password", obj.Login_PassWord);

                        if (Regex.IsMatch(obj.Login_Name, @"^1[3456789]\d{9}$"))
                        {
                            user_info.user_mobile = obj.Login_Name;
                            dynamicParams.Add("user_mobile", obj.Login_Name);
                            name = conn.RecordCount<User_Info_Entity>("where user_mobile=@user_mobile and user_state=1 ", dynamicParams);
                            if (name == 0)
                            {
                                return new Dm_ResultDefault() { Result = 0, ResultDesc = "账号错误！" };
                            }
                            num = conn.GetList<User_Info_Entity>("where user_mobile=@user_mobile and login_password=@login_password and user_state=1 ", dynamicParams).ToList();
                            if (num == null)
                            {
                                return new Dm_ResultDefault() { Result = 0, ResultDesc = "密码错误！" };
                            }
                        }
                        else
                        {
                            user_info.user_nickname = obj.Login_Name;
                            dynamicParams.Add("user_nickname", obj.Login_Name);
                            name = conn.RecordCount<User_Info_Entity>("where user_nickname=@user_nickname and user_state=1 ", dynamicParams);
                            if (name == 0)
                            {
                                return new Dm_ResultDefault() { Result = 0, ResultDesc = "账号错误！" };
                            }
                            num = conn.GetList<User_Info_Entity>("where user_nickname=@user_nickname and login_password=@login_password and user_state=1 ", dynamicParams).ToList();
                            if (num == null)
                            {
                                return new Dm_ResultDefault() { Result = 0, ResultDesc = "密码错误！" };
                            }
                        }


                        tran.Commit();

                        return new Dm_ResultDefault() { Result = 1, ResultDesc = "登录成功" };
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        throw ex;
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        /// <summary>
        /// 产生ids4 token
        /// </summary>
        /// <returns></returns>
        public static async Task<Dm_ResultDefault> Token(User_Login obj)
        {
            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync("http://localhost:10112");
            if (disco.IsError)
            {
                //Console.WriteLine(disco.Error);
                //return;
                return new Dm_ResultDefault() { Result = 0, ResultDesc = disco.Error };

            }
            var tokenResponse = await client.RequestPasswordTokenAsync(
                new PasswordTokenRequest
                {
                    Address = disco.TokenEndpoint,
                    ClientId = "client",
                    ClientSecret = "aju",
                    Scope = "api1 offline_access",
                    UserName = obj.Login_Name,
                    Password = obj.Login_PassWord
                });
            if (tokenResponse.IsError)
            {
                //Console.WriteLine(tokenResponse.Error);
                //return;
                return new Dm_ResultDefault() { Result = 0, ResultDesc = tokenResponse.Error };
            }
            //Console.WriteLine(tokenResponse.Json);
            //Console.WriteLine("\n\n");
            //call api	
            var apiClient = new HttpClient();
            apiClient.SetBearerToken(tokenResponse.AccessToken);
            var response = await apiClient.GetAsync("http://localhost:10112/api");
            if (!response.IsSuccessStatusCode)
            {
                //Console.WriteLine(response.StatusCode);
                //return new Dm_ResultDefault() { Result = 1, ResultDesc = response.StatusCode.ToString() };

                tokenResponse_Entity param = JsonConvert.DeserializeObject<tokenResponse_Entity>(tokenResponse.Json.ToString());
                return new Dm_ResultDefault() { Result = 1, ResultDesc = param.refresh_token };


            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
               // Console.WriteLine(JArray.Parse(content));
                return new Dm_ResultDefault() { Result = 0, ResultDesc = JArray.Parse(content).ToString() };

            }
            // Console.ReadLine();

        }

    }
}
