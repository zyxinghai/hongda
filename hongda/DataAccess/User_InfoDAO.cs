using Dapper;
using hongda.Entity;
using hongda.ResultEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace hongda.DataAccess
{
    /// <summary>
    /// 用户
    /// </summary>
    public class User_InfoDAO
    {
        /// <summary>
        /// 用户查询
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="comparam"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public static List<User_Info_Entity> GetList(IDbConnection conn, Dm_Paging comparam, out int recordCount)
        {
            var dynamicParams = new DynamicParameters();
            string strWhere = " where user_state=1 ";
            string strOrderBy = " order by crt_date ";
            foreach (var item in comparam.ilist)
            {
                if (item.Title?.Trim().ToLower() == "user_mobile" && !string.IsNullOrWhiteSpace(item.Content))
                {
                    strWhere += " and user_mobile = @user_mobile";
                    dynamicParams.Add("user_mobile", item.Content);
                }
                if (item.Title?.Trim().ToLower() == "user_name" && !string.IsNullOrWhiteSpace(item.Content))
                {
                    strWhere += " and user_name like @user_name";
                    dynamicParams.Add("user_name", "%" + item.Content + "%");
                }
                if (item.Title?.Trim().ToLower() == "user_nickname" && !string.IsNullOrWhiteSpace(item.Content))
                {
                    strWhere += " and user_nickname = @user_nickname";
                    dynamicParams.Add("user_nickname", item.Content);
                }
                if (item.Title?.Trim().ToLower() == "login_ip" && !string.IsNullOrWhiteSpace(item.Content))
                {
                    strWhere += " and login_ip = @login_ip";
                    dynamicParams.Add("login_ip", item.Content);
                }
                if (item.Title?.Trim().ToLower() == "login_firsttime" && !string.IsNullOrWhiteSpace(item.Content))
                {
                    strWhere += " and login_firsttime = @login_firsttime";
                    dynamicParams.Add("login_firsttime", item.Content);
                }
                if (item.Title?.Trim().ToLower() == "login_lasttime" && !string.IsNullOrWhiteSpace(item.Content))
                {
                    strWhere += " and login_lasttime = @login_lasttime";
                    dynamicParams.Add("login_lasttime", item.Content);
                }
            }

            recordCount = conn.RecordCount<User_Info_Entity>();

            if (comparam.PageSize > 0 && comparam.PageIndex > 0)
            {
                strOrderBy += $" limit {(comparam.PageIndex - 1) * comparam.PageSize}, {comparam.PageSize}";
            }

            var rst = conn.GetList<User_Info_Entity>(strWhere + strOrderBy, dynamicParams).ToList();

            return rst;
        }

    }
}
