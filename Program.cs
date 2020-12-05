using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using RemoteMonitor.Monitor;
using RemoteMonitor.DbHandlers;
using System;

namespace RemoteMonitor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args[0] == "server")
            {
                CreateHostBuilder(args).Build().Run();
            }
            else
            {
                Console.WriteLine("Monitoring Resource Usage");
                MonitorResourceUsage usage = new MonitorResourceUsage();
                usage.SaveUsage();
            }
            
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
