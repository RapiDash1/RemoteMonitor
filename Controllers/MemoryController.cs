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
                if (analytics.IsResourceUsagePeak(usage.resourceUsage))
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
                if (analytics.IsResourceUsageLowest(usage.resourceUsage))
                {
                    peakUsages.Add(usage);
                }
            }
            return peakUsages;
        }

        [HttpGet("longestpeakusage")]
        public List<string> longestPeakUsageDuraiton()
        {
            List<MemoryUsageModel> dailyUsages = this.Daily();
            List<MemoryUsageModel> peakUsages = new List<MemoryUsageModel>();
            int longestDuration = 1;
            for (int i=0; i< dailyUsages.Count-1; i++)
            {
                int nextPeakUsagePos = i+1;
                while (analytics.IsResourceUsagePeak(dailyUsages[nextPeakUsagePos].resourceUsage))
                {
                    nextPeakUsagePos += 1;
                }
                if (nextPeakUsagePos-i > longestDuration)
                {
                    peakUsages.Clear();
                    peakUsages.Add(dailyUsages[i]);
                    peakUsages.Add(dailyUsages[nextPeakUsagePos-1]);
                }
                i = nextPeakUsagePos-1;
            }
            List<string> peakEpochs = new List<string>();
            foreach (MemoryUsageModel usage in peakUsages)
            {
                peakEpochs.Add(usage.epocTime.ToString());
            }
            return peakEpochs;
        }
    }
}