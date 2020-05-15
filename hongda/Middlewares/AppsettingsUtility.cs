using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hongda.Middlewares
{
    /// <summary>
    /// 配置实体
    /// </summary>
    public class AppsettingsUtility
    {
        private static configure _myOwn;
        /// <summary>
        /// 
        /// </summary>
        public static configure MyOwn { get { return _myOwn; } }
        private static ConnectionStrings _connction;
        /// <summary>
        /// 
        /// </summary>
        public static ConnectionStrings Connction { get { return _connction; } }



        /// <summary>
        /// 将配置项的值赋值给属性
        /// </summary>
        /// <param name="configuration"></param>
        public void Initial(IConfiguration configuration)
        {
            configure myOwn = new configure();
            //注意：可以使用冒号来获取内层的配置项
            myOwn.AccessKeyID = configuration["configure:AccessKeyID"];
            myOwn.AccessKeySecret = configuration["configure:AccessKeySecret"];
            myOwn.upload = configuration["configure:upload"];
            myOwn.FileDownloadBasePath = configuration["configure:FileDownloadBasePath"];
            myOwn.FullName = configuration["configure:FullName"];
            myOwn.dest = configuration["configure:dest"];
            myOwn.filename = configuration["configure:filename"];
            myOwn.tjFullName = configuration["configure:tjFullName"];
            _myOwn = myOwn;


            //数据库连接
            ConnectionStrings Connction = new ConnectionStrings();
            Connction.conn = configuration["ConnectionStrings:conn"];
            _connction = Connction;

        }



    }
}
