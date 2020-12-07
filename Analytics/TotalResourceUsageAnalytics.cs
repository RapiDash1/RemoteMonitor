using RemoteMonitor.Usage;
using RemoteMonitor.Models;
using RemoteMonitor.DbHandlers;
using System.Collections.Generic;

namespace RemoteMonitor.Analytics
{   
    class TotalResourceUsageAnalytics
    {
        private CpuUsageAnalytics cpuUsageAnalytics = new CpuUsageAnalytics();

        private MemoryUsageAnalytics memoryUsageAnalytics = new MemoryUsageAnalytics();

        public bool IsResourceUsagePeak(TotalResourceUsageModel usage)
        {
            return  cpuUsageAnalytics.IsResourceUsagePeak(usage.cpuUsage) || 
                        memoryUsageAnalytics.IsResourceUsagePeak(usage.memoryAvailable);
        }

        public bool IsResourceUsageLowest(TotalResourceUsageModel usage)
        {
            return  cpuUsageAnalytics.IsResourceUsageLowest(usage.cpuUsage) || 
                        memoryUsageAnalytics.IsResourceUsageLowest(usage.memoryAvailable);
        }

        public string Current()
        {
            return "Cpu usage: "+ cpuUsageAnalytics.Current() + "% || " + memoryUsageAnalytics.Current() + " MB";
        }
    }
}