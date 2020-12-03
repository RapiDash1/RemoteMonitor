using RemoteMonitor.Usage;

namespace RemoteMonitor.Analytics
{   
    class CpuUsageAnalytics : ResourceUsageAnalytics
    {
        public CpuUsageAnalytics() : base()
        {
            base.resourceUsage = new CpuUsage();
        }
    }
}