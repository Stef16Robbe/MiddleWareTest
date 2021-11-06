using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWareTest
{
    public class GoogleAuthReqBody
    {
        public string credential { get; set; }
        public string g_csrf_token { get; set; }
    }
}
