using System;
using System.Collections.Generic;
using System.Text;

namespace hongda.Entity
{
    /// <summary>
    /// ids4
    /// </summary>
    public class tokenResponse_Entity
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public string token_type { get; set; }
        public string refresh_token { get; set; }
        public string scope { get; set; }
    }
}
