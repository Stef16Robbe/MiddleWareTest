using MiddleWareTest.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWareTest
{
    public class Helpers
    {
        public static AuthorizedAttribute GetAuthorizedAttributes<T>(string method) where T : class
        {
            try
            {
                return (AuthorizedAttribute)typeof(T).GetMethod(method).GetCustomAttributes(typeof(AuthorizedAttribute), false).FirstOrDefault();
            }
            catch (SystemException)
            {
                return null;
            }
        }        
    }
}
