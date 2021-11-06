using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace MiddleWareTest
{
    public class LoginFunction
    {
        [Function("Login")]
        public static HttpResponseData Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req,
            FunctionContext context)
        {
            throw new Exception("yo");
        }
    }
}
