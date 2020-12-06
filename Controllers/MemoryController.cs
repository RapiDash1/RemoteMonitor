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
            return this.ExtremeUsage(true);
        }

        [HttpGet("lowestusages")]
        public List<MemoryUsageModel> LowestUsages()
        {
            return this.ExtremeUsage(false);
        }

        [HttpGet("longestpeakusage")]
        public List<string> LongestPeakUsageDuraiton()
        {
            return this.ExtremeUsageDuration(true);
        }

        [HttpGet("longestlowestusage")]
        public List<string> LongestLowestUsageDuraiton()
        {
            return this.ExtremeUsageDuration(false);
        }


        public bool IsUsageExtreme(float memoryUsageValue, bool peak=true)
        {
            if (peak)
            {
                return analytics.IsResourceUsagePeak(memoryUsageValue);
            }
            return analytics.IsResourceUsageLowest(memoryUsageValue);
        }

        public List<MemoryUsageModel> ExtremeUsage(bool peak=true)
        {
            List<MemoryUsageModel> dailyUsages = this.Daily();
            List<MemoryUsageModel> peakUsages = new List<MemoryUsageModel>();

            foreach(MemoryUsageModel usage in dailyUsages)
            {
                if (this.IsUsageExtreme(usage.resourceUsage, peak))
                {
                    peakUsages.Add(usage);
                }
            }
            return peakUsages;
        }

        public List<string> ExtremeUsageDuration(bool peak=true)
        {
            List<MemoryUsageModel> dailyUsages = this.Daily();
            List<MemoryUsageModel> peakUsages = new List<MemoryUsageModel>();
            int longestDuration = 1;
            for (int i=0; i< dailyUsages.Count-1; i++)
            {
                int nextPeakUsagePos = i+1;
                while (this.IsUsageExtreme(dailyUsages[nextPeakUsagePos].resourceUsage, peak))
                {
                    nextPeakUsagePos += 1;
                }
                if (nextPeakUsagePos-i > longestDuration)
                {
                    peakUsages.Clear();
                    peakUsages.Add(dailyUsages[i]);
                    peakUsages.Add(dailyUsages[nextPeakUsagePos-1]);
                    longestDuration = nextPeakUsagePos-i;
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