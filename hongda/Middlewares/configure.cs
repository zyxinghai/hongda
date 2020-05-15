using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hongda.Middlewares
{
    public class configure
    {
        public string AccessKeyID { get; set; }
        public string AccessKeySecret { get; set; }
        public string upload { get; set; }
        public string FileDownloadBasePath { get; set; }
        public string FullName { get; set; }
        public string dest { get; set; }
        public string filename { get; set; }
        public string tjFullName { get; set; }
    }

    public class ConnectionStrings
    {
        public string conn { get; set; }
    }

    public class total
    {
        public string count { get; set; }
    }
}
