using RemoteMonitor.Usage;
using RemoteMonitor.Models;
using RemoteMonitor.DbHandlers;
using System.Collections.Generic;

namespace RemoteMonitor.Analytics
{   
    class ResourceUsageAnalytics
    {
        public float peakUsageThreshold { get; set; } = 90;
        public float lowestUsageThreshold { get; set; } = 5;

        protected ResourceUsage resourceUsage;

        public virtual bool IsResourceUsagePeak(float resourceUsage)
        {
            return resourceUsage >= peakUsageThreshold;
        }

        public virtual bool IsResourceUsageLowest(float resourceUsage)
        {
            return resourceUsage <= lowestUsageThreshold;
        }

        public virtual ResourceUsageModel[] resourceUsagesPerDay()
        {
            return new ResourceUsageModel[]{};
        }

        public float longestPeakUsageDuration()
        {
            return 0;
        }

        public float longestLowestUsageDuration()
        {
            return 0;
        }

        public float Current()
        {
            return resourceUsage.Current();
        }
    }
}