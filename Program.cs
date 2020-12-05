using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using RemoteMonitor.Monitor;

namespace RemoteMonitor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // CreateHostBuilder(args).Build().Run();
            MonitorResourceUsage usage = new MonitorResourceUsage();
            // usage.SaveUsage();
            usage.GetResourceUsage();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
