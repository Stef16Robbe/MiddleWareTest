using Microsoft.Extensions.Hosting;

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
                    }
                )
                .Build();

            host.Run();
        }
    }
}