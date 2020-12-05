using RemoteMonitor.Usage;
using RemoteMonitor.Models;
using System.Collections.Generic;
using RemoteMonitor.DbHandlers;

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

        public int TotalMemory()
        {
            return 16000;
        }

        public override bool IsResourceUsagePeak(float resourceUsage)
        {
            return this.TotalMemory() - resourceUsage >= peakUsageThreshold;
        }

        public override bool IsResourceUsageLowest(float resourceUsage)
        {
            return this.TotalMemory() - resourceUsage <= lowestUsageThreshold;
        }

        public List<MemoryUsageModel> Daily()
        {
            MemoryUsageDbQuery memoryUsageDbQuery = new MemoryUsageDbQuery();
            return memoryUsageDbQuery.GetDailyUsage();
        }
    }
}