using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hongda.ResultEntities
{
    /// <summary>
    /// 返回
    /// </summary>
    public class Dm_ResultDefault
    {
        /// <summary>
        /// 结果
        /// </summary>
        public int Result { get; set; }
        /// <summary>
        /// 结果信息
        /// </summary>
        public string ResultDesc { get; set; }
    }
    /// <summary>
    /// 返回集合和数量
    /// </summary>
    public class Dm_ResultList : Dm_ResultDefault
    {
        /// <summary>
        /// 结果集合
        /// </summary>
        public IList ResultList { get; set; }
        /// <summary>
        /// 总数
        /// </summary>
        public int TotalRecords { get; set; }

    }
}
