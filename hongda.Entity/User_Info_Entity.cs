using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hongda.Entity
{
    /// <summary>
    /// 用户登录入参
    /// </summary>
    [Table("user_info")]
    public class User_Info_Entity
    {
        /// <summary>
        /// 用户id
        /// </summary>
        [Key]
        public Guid user_id { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string user_name { get; set; }
        /// <summary>
        /// 用户昵称
        /// </summary>
        public string user_nickname { get; set; }
        /// <summary>
        /// 用户身份证号
        /// </summary>
        public string user_idnumber { get; set; }
        /// <summary>
        /// 用户手机号
        /// </summary>
        public string user_mobile { get; set; }
        /// <summary>
        /// 用户邮箱
        /// </summary>
        public string user_email { get; set; }
        /// <summary>
        /// 用户图片
        /// </summary>
        public Guid user_avatar_id { get; set; }
        /// <summary>
        /// 用户密码
        /// </summary>
        public string login_password { get; set; }
        /// <summary>
        /// 用户首次登录时间
        /// </summary>
        public DateTime login_firsttime { get; set; }
        /// <summary>
        /// 用户最近一次登录时间
        /// </summary>
        public DateTime login_lasttime { get; set; }
        /// <summary>
        /// 用户ip
        /// </summary>
        public string login_ip { get; set; }
        /// <summary>
        /// 标志
        /// </summary>
        public int user_state { get; set; }
        /// <summary>
        /// 用户类型
        /// </summary>
        public int user_type { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public Guid crt_user { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime crt_date { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        public Guid up_user { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime up_date { get; set; }

    }

    /// <summary>
    /// 登录入参
    /// </summary>
    public class User_Login
    {
        /// <summary>
        /// 登录账号
        /// </summary>
        public string Login_Name { get; set; }
        /// <summary>
        /// 登录密码
        /// </summary>
        public string Login_PassWord { get; set; }
        /// <summary>
        /// 登录ip
        /// </summary>
        public string Login_Ip { get; set; }
    }
}
