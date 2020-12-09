using RemoteMonitor.Usage;
using RemoteMonitor.Models;
using System.Collections.Generic;
using RemoteMonitor.DbHandlers;
using System.Linq;

namespace RemoteMonitor.Analytics
{   
    class MemoryUsageAnalytics : ResourceUsageAnalytics
    {
        public MemoryUsageAnalytics() : base()
        {
            this.resourceUsage = new MemoryUsage();
            base.peakUsageThreshold = 12000;
            base.lowestUsageThreshold = 1000;
        }

        /// <summary>
        /// Total Memory of the server
        /// </summary>
        /// <returns>
        /// An int returning the total memory capacity of the server
        /// </returns>
        public int TotalMemory()
        {
            return 16000;
        }

        /// <summary>
        /// Check if Memory usage is peak
        /// </summary>
        /// <param name="memoryUsage">memoryUsage for which peak-check should be made</param>
        /// <returns>
        /// A bool indicating whether Memory usage is peak
        /// </returns>
        public override bool IsResourceUsagePeak(float memoryUsage)
        {
            return this.TotalMemory() - memoryUsage >= peakUsageThreshold;
        }

        /// <summary>
        /// Check if Memory usage is low
        /// </summary>
        /// <param name="memoryUsage">memoryUsage for which low-check should be made</param>
        /// <returns>
        /// A bool indicating whether Memory usage is low
        /// </returns>
        public override bool IsResourceUsageLowest(float memoryUsage)
        {
            return this.TotalMemory() - memoryUsage <= lowestUsageThreshold;
        }

        /// <summary>
        /// Daily memory usages
        /// </summary>
        /// <returns>
        /// A list of MemoryUsageModel
        /// </returns>
        public List<MemoryUsageModel> Daily()
        {
            MemoryUsageDbQuery memoryUsageDbQuery = new MemoryUsageDbQuery();
            return memoryUsageDbQuery.GetDailyUsage().Cast<MemoryUsageModel>().ToList();
        }
    }
}