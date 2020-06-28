using hongda.DataAccess;
using hongda.Entity;
using hongda.Middlewares;
using hongda.ResultEntities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace hongda.Services
{
    /// <summary>
    /// 用户
    /// </summary>
    public class User_InfoBLO
    {
        /// <summary>
        /// 用户查询
        /// </summary>
        /// <param name="comParas"></param>
        /// <returns></returns>
        public static Dm_ResultList Query(Dm_Paging comParas)
        {
            try
            {
                try
                {
                    using (IDbConnection conn = new MySqlConnection(DapperHelper.CONNECTIONSTRING))
                    {

                        int recordCount = 0;

                        List<User_Info_Entity> lst = User_InfoDAO.GetList(conn, comParas, out recordCount);
                        if (lst == null)
                        {
                            lst = new List<User_Info_Entity>();
                        }
                        return new Dm_ResultList() { Result = 1, ResultList = lst, TotalRecords = recordCount, ResultDesc = "用户列表查询成功" };
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
