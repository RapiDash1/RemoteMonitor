using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using RemoteMonitor.Usage;
using RemoteMonitor.Models;
using RemoteMonitor.DbHandlers;
using RemoteMonitor.Analytics;

namespace RemoteMonitor.Controller
{
    [Route("cpu/")]
    [ApiController]
    public class CpuController : ControllerBase
    {
        private CpuUsageAnalytics analytics = new CpuUsageAnalytics();

        [HttpGet("current")]
        public float Current()
        {
            CpuUsage cpuUsage = new CpuUsage();
            return cpuUsage.Current();
        }

        [HttpGet("daily")]
        public List<CpuUsageModel> Daily()
        {
            CpuUsageDbQuery cpuUsageDbQuery = new CpuUsageDbQuery();
            return cpuUsageDbQuery.GetDailyUsage();
        }

        [HttpGet("peakusages")]
        public List<CpuUsageModel> PeakUsages()
        {
            List<CpuUsageModel> dailyUsages = this.Daily();
            List<CpuUsageModel> peakUsages = new List<CpuUsageModel>();

            foreach(CpuUsageModel usage in dailyUsages)
            {
                if (analytics.IsResourceUsagePeak(usage.cpuUsage))
                {
                    peakUsages.Add(usage);
                }
            }
            return peakUsages;
        }

        [HttpGet("lowestusages")]
        public List<CpuUsageModel> LowestUsages()
        {
            List<CpuUsageModel> dailyUsages = this.Daily();
            List<CpuUsageModel> peakUsages = new List<CpuUsageModel>();

            foreach(CpuUsageModel usage in dailyUsages)
            {
                if (analytics.IsResourceUsageLowest(usage.cpuUsage))
                {
                    peakUsages.Add(usage);
                }
            }
            return peakUsages;
        }
    }
}