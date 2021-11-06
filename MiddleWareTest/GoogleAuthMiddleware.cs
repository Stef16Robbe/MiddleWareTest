using Google.Apis.Auth;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker.Middleware;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MiddleWareTest
{
    public class GoogleAuthMiddleware : IFunctionsWorkerMiddleware
    {
        public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
        {
            var logger = context.GetLogger(context.FunctionDefinition.Name);
            logger.LogWarning("Executed GoogleAuthMiddleware");
            var req = MiddlewareHelper.GetHttpRequestData(context);

            // auth
            // https://developers.google.com/identity/gsi/web/guides/verify-google-id-token
            var content = await new StreamReader(req.Body).ReadToEndAsync();
            var googleJWT = HttpUtility.ParseQueryString(content).Get("credential");

            var validPayload = await GoogleJsonWebSignature.ValidateAsync(googleJWT);
            // validPayload contains email, name etc...
            // TODO:
            // user management

            await next(context);
        }
    }
}
