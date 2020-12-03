using RemoteMonitor.Usage;
using RemoteMonitor.Models;

namespace RemoteMonitor.Analytics
{   
    class CpuUsageAnalytics : ResourceUsageAnalytics
    {
        public CpuUsageAnalytics()
        {
            this.resourceUsage = new CpuUsage();
        }

        public override ResourceUsageModel[] resourceUsagesPerDay()
        {
            return new CpuUsageModel[]{};
        }
    }
}