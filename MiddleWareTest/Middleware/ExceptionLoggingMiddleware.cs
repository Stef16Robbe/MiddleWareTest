using System;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Middleware;
using Microsoft.Extensions.Logging;

namespace MiddleWareTest.Middleware
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
                logger.LogError("Unexpected Error in {FunctionName}: {ExceptionMessage}", context.FunctionDefinition.Name, ex.Message);
            }
        }
    }
}