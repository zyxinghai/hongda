using Dapper;
using hongda.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace hongda.DataAccess
{
    /// <summary>
    /// 登录查询
    /// </summary>
    public class User_LoginDAO
    {
        /// <summary>
        /// token 新增
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="tran"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static Guid? TokenAdd(IDbConnection conn, IDbTransaction tran, Check_Token_Entity entity)
        {
            return conn.Insert<Guid, Check_Token_Entity>(entity, tran);
        }

        /// <summary>
        /// 第一次登录修改用户
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="tran"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static int uieUpdate(IDbConnection conn, IDbTransaction tran, User_Info_Entity entity)
        {
            return conn.Execute("update user_info set login_firsttime=@login_firsttime,login_ip=@login_ip,up_user=@up_user,up_date=@up_date where user_id=@user_id", entity, tran);
        }

        /// <summary>
        /// 登录修改用户
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="tran"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static int uieUpdates(IDbConnection conn, IDbTransaction tran, User_Info_Entity entity)
        {
            return conn.Execute("update user_info set login_lasttime=@login_lasttime,login_ip=@login_ip,up_user=@up_user,up_date=@up_date where user_id=@user_id", entity, tran);
        }


    }
}
