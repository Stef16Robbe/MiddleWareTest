using System;
using System.Linq;
using MiddleWareTest.Domain;

namespace MiddleWareTest
{
    public class Helpers
    {
        public static AuthorizedAttribute GetAuthorizedAttributes<T>(string method) where T : class
        {
            try
            {
                return (AuthorizedAttribute) typeof(T).GetMethod(method)
                    ?.GetCustomAttributes(typeof(AuthorizedAttribute), false).FirstOrDefault();
            }
            catch (SystemException)
            {
                return null;
            }
        }
    }
}