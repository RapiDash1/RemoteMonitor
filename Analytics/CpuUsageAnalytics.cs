using RemoteMonitor.Usage;
using RemoteMonitor.Models;
using System.Collections.Generic;
using RemoteMonitor.DbHandlers;
using System.Linq;

namespace RemoteMonitor.Analytics
{   
    class CpuUsageAnalytics : ResourceUsageAnalytics
    {
        public CpuUsageAnalytics() : base()
        {
            this.resourceUsage = new CpuUsage();
        }

        public List<CpuUsageModel> Daily()
        {
            CpuUsageDbQuery cpuUsageDbQuery = new CpuUsageDbQuery();
            return cpuUsageDbQuery.GetDailyUsage().Cast<CpuUsageModel>().ToList();
        }
    }
}