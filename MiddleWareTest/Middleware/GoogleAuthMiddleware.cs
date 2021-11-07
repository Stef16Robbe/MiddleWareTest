using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using Google.Apis.Auth;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Middleware;
using Microsoft.Extensions.Logging;

namespace MiddleWareTest.Middleware
{
    public class GoogleAuthMiddleware : IFunctionsWorkerMiddleware
    {
        public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
        {
            var logger = context.GetLogger(context.FunctionDefinition.Name);
            logger.LogWarning("Executed GoogleAuthMiddleware");
            var req = context.GetHttpRequestData();

            // auth
            // https://developers.google.com/identity/gsi/web/guides/verify-google-id-token
            var content = await new StreamReader(req.Body).ReadToEndAsync();
            var googleJwt = HttpUtility.ParseQueryString(content).Get("credential");

            var validPayload = await GoogleJsonWebSignature.ValidateAsync(googleJwt);

            // validPayload contains email, name etc...
            // pass the current user email down to the function being called
            context.Items.Add(new KeyValuePair<object, object>("Email", validPayload.Email));

            await next(context);
        }
    }
}