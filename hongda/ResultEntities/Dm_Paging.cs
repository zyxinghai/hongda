using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hongda.ResultEntities
{
    /// <summary>
    /// 输入参数
    /// </summary>
    public class Dm_Paging
    {
        /// <summary>
        /// 页数
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 条数
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 参数list
        /// </summary>
        public IList<Dm_QueryItem> ilist { get; set; }
    }
    /// <summary>
    /// 条件参数
    /// </summary>
    public class Dm_QueryItem
    {
        /// <summary>
        /// 参数名
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 参数值
        /// </summary>
        public string Content { get; set; }
    }
}
