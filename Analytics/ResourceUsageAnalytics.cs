using System;
using RemoteMonitor.Usage;

namespace RemoteMonitor.Analytics
{   
    class ResourceUsageAnalytics
    {
        public float peakUsageThreshold { get; set; } = 90;
        public float lowestUsageThreshold { get; set; } = 5;

        protected ResourceUsage resourceUsage;

        public bool IsResourceUsagePeak()
        {
            return resourceUsage.Current() >= peakUsageThreshold;
        }

        public bool IsResourceUsageLowest()
        {
            return resourceUsage.Current() <= lowestUsageThreshold;
        }

        public void SaveResourceUsage()
        {
            // Save resource usage to db
        }


    }
}