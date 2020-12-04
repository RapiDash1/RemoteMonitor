using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace RemoteMonitor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // CreateHostBuilder(args).Build().Run();
            // CpuUsage cpuUsage = new CpuUsage();
            // Console.WriteLine(cpuUsage.Current());
            // MemoryUsage memoryUsage = new MemoryUsage();
            // Console.WriteLine(memoryUsage.Current());
            // CpuUsageAnalytics cpuUsageAnalytics = new CpuUsageAnalytics();
            // cpuUsageAnalytics.SaveResourceUsage(@"INSERT INTO ResourceUsage (EpocTime, CpuUsage, MemoryUsage) VALUES ('1098997245623222','34.7','14008');");
            // cpuUsageAnalytics.GetResourceUsage(@"SELECT * from ResourceUsage");
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
