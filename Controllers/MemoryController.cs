using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using RemoteMonitor.Usage;
using RemoteMonitor.Models;
using RemoteMonitor.DbHandlers;
using RemoteMonitor.Analytics;

namespace RemoteMonitor.Controller
{
    [Route("memory/")]
    [ApiController]
    public class MemoryController : ControllerBase
    {
        private MemoryUsageAnalytics analytics = new MemoryUsageAnalytics();
        
        [HttpGet("current")]
        public float Current()
        {
            return analytics.Current();
        }

        [HttpGet("daily")]
        public List<MemoryUsageModel> Daily()
        {
            return analytics.Daily();
        }

        [HttpGet("peakusages")]
        public List<MemoryUsageModel> PeakUsages()
        {
            List<MemoryUsageModel> dailyUsages = this.Daily();
            List<MemoryUsageModel> peakUsages = new List<MemoryUsageModel>();

            foreach(MemoryUsageModel usage in dailyUsages)
            {
                if (analytics.IsResourceUsagePeak(usage.memoryAvailable))
                {
                    peakUsages.Add(usage);
                }
            }
            return peakUsages;
        }

        [HttpGet("lowestusages")]
        public List<MemoryUsageModel> LowestUsages()
        {
            List<MemoryUsageModel> dailyUsages = this.Daily();
            List<MemoryUsageModel> peakUsages = new List<MemoryUsageModel>();

            foreach(MemoryUsageModel usage in dailyUsages)
            {
                if (analytics.IsResourceUsageLowest(usage.memoryAvailable))
                {
                    peakUsages.Add(usage);
                }
            }
            return peakUsages;
        }
    }
}