using RemoteMonitor.Usage;
using RemoteMonitor.Models;

namespace RemoteMonitor.Analytics
{   
    class MemoryUsageAnalytics : ResourceUsageAnalytics
    {
        public MemoryUsageAnalytics()
        {
            this.resourceUsage = new MemoryUsage();
        }

        public override ResourceUsageModel[] resourceUsagesPerDay()
        {
            return new MemoryUsageModel[]{};
        }
    }
}