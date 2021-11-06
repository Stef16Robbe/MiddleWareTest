using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWareTest
{
    public class UnauthorizedGoogleException: Exception
    {
        public UnauthorizedGoogleException(string originalMessage = "") : base("You are unauthorized to perform this request.")
        {
        }
    }
}
