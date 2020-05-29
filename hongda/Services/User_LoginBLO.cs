using Dapper;
using hongda.Entity;
using hongda.Middlewares;
using hongda.ResultEntities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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

                    conn.Open();
                    IDbTransaction tran = conn.BeginTransaction();
                    try
                    {
                        User_Info_Entity user_info = new User_Info_Entity();
                        var dynamicParams = new DynamicParameters();
                        int num = 0;
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
                            num =conn.RecordCount<User_Info_Entity>("where user_mobile=@user_mobile and login_password=@login_password and user_state=1 ", dynamicParams);
                            if (num == 0)
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
                            num = conn.RecordCount<User_Info_Entity>("where user_nickname=@user_nickname and login_password=@login_password and user_state=1 ", dynamicParams);
                            if (num == 0)
                            {
                                return new Dm_ResultDefault() { Result = 0, ResultDesc = "密码错误！" };
                            }
                        }

                        if(num==1)
                        {

                        }
                        else
                        {
                            return new Dm_ResultDefault() { Result = 0, ResultDesc = "数据错误，请联系管理员！" };
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


    }
}
