using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace hongda.Entity
{
    /// <summary>
    /// token表
    /// </summary>
    [Table("check_token")]
    public class Check_Token_Entity
    {
        /// <summary>
        /// token  id
        /// </summary>
        [Key]
        public Guid cktoken_id { get; set; }
        /// <summary>
        /// token
        /// </summary>
        public string cktoken_token { get; set; }
        /// <summary>
        /// token 人员id
        /// </summary>
        public Guid cktoken_ur_id { get; set; }
        /// <summary>
        /// token时间
        /// </summary>
        public DateTime cktoken_time { get; set; }
        /// <summary>
        /// token  ip
        /// </summary>
        public string cktoken_ip { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int cktoken_state { get; set; }

    }
}
