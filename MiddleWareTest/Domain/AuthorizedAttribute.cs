using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWareTest.Domain
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AuthorizedAttribute : Attribute
    {
        public UserType[] AllowedUserTypes { get; private set; }

        public AuthorizedAttribute(UserType[] types)
        {
            AllowedUserTypes = types;
        }
    }    
}
