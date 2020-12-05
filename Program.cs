using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using RemoteMonitor.Monitor;
using RemoteMonitor.DbHandlers;

namespace RemoteMonitor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // CreateHostBuilder(args).Build().Run();
            MemoryUsageDbQuery usage = new MemoryUsageDbQuery();
            // usage.SaveUsage();
            usage.GetMemoryUsage();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
