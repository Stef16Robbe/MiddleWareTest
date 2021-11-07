using System;

namespace MiddleWareTest.Domain
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AuthorizedAttribute : Attribute
    {
        public AuthorizedAttribute(UserType[] types)
        {
            AllowedUserTypes = types;
        }

        public UserType[] AllowedUserTypes { get; }
    }
}