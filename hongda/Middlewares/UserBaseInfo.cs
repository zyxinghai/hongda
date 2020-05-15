using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hongda.Middlewares
{
    public class UserBaseInfo
    {
        /// <summary>
        /// 登录用户ID
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 登录用户名称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 所属单位ID
        /// </summary>
        public Guid OrgId { get; set; }
        /// <summary>
        /// 运营主体ID
        /// </summary>
        public Guid subsys_org_id { get; set; }
        /// <summary>
        /// 所属组织机构名称
        /// </summary>
        public string cktoken_og_name { get; set; }
    }
}
