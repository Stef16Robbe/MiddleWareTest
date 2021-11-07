using Microsoft.Extensions.Hosting;
using MiddleWareTest.Middleware;

namespace MiddleWareTest
{
    public class Program
    {
        public static void Main()
        {
            var host = new HostBuilder()
                .ConfigureFunctionsWorkerDefaults(
                    builder =>
                    {
                        builder.UseMiddleware<ExceptionLoggingMiddleware>();
                        builder.UseMiddleware<GoogleAuthMiddleware>();
                    }
                )
                .Build();

            host.Run();
        }
    }
}