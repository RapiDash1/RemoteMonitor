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

        /// <summary>
        /// Check if Total Resource usage is peak
        /// </summary>
        /// <param name="usage">usage for which peak-check should be made</param>
        /// <returns>
        /// A bool indicating whether Total Resource usage is peak
        /// </returns>
        public bool IsResourceUsagePeak(TotalResourceUsageModel usage)
        {
            return  cpuUsageAnalytics.IsResourceUsagePeak(usage.cpuUsage) || 
                        memoryUsageAnalytics.IsResourceUsagePeak(usage.memoryAvailable);
        }

        /// <summary>
        /// Check if Total Resource usage is low
        /// </summary>
        /// <param name="usage">usage for which low-check should be made</param>
        /// <returns>
        /// A bool indicating whether resource usage is low
        /// </returns>
        public bool IsResourceUsageLowest(TotalResourceUsageModel usage)
        {
            return  cpuUsageAnalytics.IsResourceUsageLowest(usage.cpuUsage) || 
                        memoryUsageAnalytics.IsResourceUsageLowest(usage.memoryAvailable);
        }

        /// <summary>
        /// Get Current Total Resource Usage
        /// </summary>
        /// <returns>
        /// A string containing the CPU and Memory usage
        /// </returns>
        public string Current()
        {
            return "Cpu usage: "+ cpuUsageAnalytics.Current() + "% || Memory Available: " + memoryUsageAnalytics.Current() + " MB";
        }
    }
}