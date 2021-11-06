using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Middleware;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace MiddleWareTest
{
    public class ExceptionLoggingMiddleware : IFunctionsWorkerMiddleware
    {
        public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
        {
            var logger = context.GetLogger(context.FunctionDefinition.Name);
            try
            {
                logger.LogWarning("Executed ExceptionLoggingMiddleware");
                await next(context);
            }
            catch (Exception ex)
            {
                logger.LogError("Unexpected Error in {0}: {1}", context.FunctionDefinition.Name, ex.Message);
            }
        }
    }
}
