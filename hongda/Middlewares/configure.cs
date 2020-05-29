using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hongda.Middlewares
{
    /// <summary>
    /// 配置
    /// </summary>
    public class configure
    {
        /// <summary>
        /// 秘钥
        /// </summary>
        public string AccessKeyID { get; set; }
        /// <summary>
        /// 秘钥
        /// </summary>
        public string AccessKeySecret { get; set; }
        /// <summary>
        /// 上传地址
        /// </summary>
        public string upload { get; set; }
        /// <summary>
        /// 下载地址
        /// </summary>
        public string FileDownloadBasePath { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string dest { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string filename { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string tjFullName { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class ConnectionStrings
    {
        /// <summary>
        /// 数据库连接
        /// </summary>
        public string conn { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class total
    {
        /// <summary>
        /// 
        /// </summary>
        public string count { get; set; }
    }
}
