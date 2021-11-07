using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace MiddleWareTest.Functions
{
    public class LoginFunction
    {
        [Function("Login")]
        public static HttpResponseData Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req,
            FunctionContext context)
        {
            // login magic
            throw new Exception(
                "Logged in. I can throw this exception cause I have exception handling in my Middleware B)");
        }
    }
}