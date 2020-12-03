using RemoteMonitor.Usage;
using RemoteMonitor.Models;

namespace RemoteMonitor.Analytics
{   
    class ResourceUsageAnalytics
    {
        public float peakUsageThreshold { get; set; } = 90;
        public float lowestUsageThreshold { get; set; } = 5;

        protected ResourceUsage resourceUsage = new CpuUsage();

        public bool IsResourceUsagePeak()
        {
            return resourceUsage.Current() >= peakUsageThreshold;
        }

        public bool IsResourceUsageLowest()
        {
            return resourceUsage.Current() <= lowestUsageThreshold;
        }

        public virtual ResourceUsageModel[] resourceUsagesPerDay()
        {
            return new ResourceUsageModel[]{};
        }

        public long longestPeakUsageDuration()
        {
            return 0;
        }

        public long longestLowestUsageDuration()
        {
            return 0;
        }
    }
}