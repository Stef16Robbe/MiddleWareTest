using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using Google.Apis.Auth;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Middleware;
using Microsoft.Extensions.Logging;
using MiddleWareTest.Domain.Exceptions;

namespace MiddleWareTest.Middleware
{
    public class GoogleAuthMiddleware : IFunctionsWorkerMiddleware
    {
        public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
        {
            var logger = context.GetLogger(context.FunctionDefinition.Name);
            logger.LogWarning("Executed GoogleAuthMiddleware");
            var req = context.GetHttpRequestData();

            var emailFunctions = new List<string>()
            {
                "",
                ""
            };

            // auth
            // https://developers.google.com/identity/gsi/web/guides/verify-google-id-token
            var content = await new StreamReader(req.Body).ReadToEndAsync();
            var googleJwt = HttpUtility.ParseQueryString(content).Get("credential");

            // https://cloud.google.com/dotnet/docs/reference/Google.Apis/latest/Google.Apis.Auth.GoogleJsonWebSignature#Google_Apis_Auth_GoogleJsonWebSignature_ValidateAsync_System_String_Google_Apis_Util_IClock_System_Boolean_
            var validPayload = await GoogleJsonWebSignature.ValidateAsync(googleJwt);

            // validPayload contains email, name etc...
            // pass the current user email down to the function being called
            if (!validPayload.EmailVerified) throw new UnauthorizedGoogleException();

            if (emailFunctions.Contains(context.FunctionDefinition.Name))
                context.Items.Add(new KeyValuePair<object, object>("GoogleEmail", validPayload.Email));
            
            context.Items.Add(new KeyValuePair<object, object>("GoogleId", validPayload.Subject));

            await next(context);
        }
    }
}