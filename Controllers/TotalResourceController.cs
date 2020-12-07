using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using RemoteMonitor.Usage;
using RemoteMonitor.Models;
using RemoteMonitor.DbHandlers;
using RemoteMonitor.Analytics;
using System.Linq;

namespace RemoteMonitor.Controller
{
    [Route("totalresources/")]
    [ApiController]
    public class TotalResourceController : ControllerBase
    {
        private TotalResourceUsageAnalytics analytics = new TotalResourceUsageAnalytics();

        [HttpGet("current")]
        public string Current()
        {
            return analytics.Current();
        }

        [HttpGet("daily")]
        public List<TotalResourceUsageModel> Daily()
        {
            TotalResourceUsageDbHandler usageDbHandler = new TotalResourceUsageDbHandler();
            return usageDbHandler.GetDailyUsage();
        }

        [HttpGet("peakusages")]
        public List<TotalResourceUsageModel> PeakUsages()
        {
            return this.ExtremeUsage(true);
        }

        [HttpGet("lowestusages")]
        public List<TotalResourceUsageModel> LowestUsages()
        {
            return this.ExtremeUsage(false);
        }

        [HttpGet("longestpeakusage")]
        public List<string> longestPeakUsageDuraiton()
        {
            return this.ExtremeUsageDuration(true);
        }

        [HttpGet("longestlowestusage")]
        public List<string> longestLowestUsageDuraiton()
        {
            return this.ExtremeUsageDuration(false);
        }


        public bool IsUsageExtreme(TotalResourceUsageModel resourceUsage, bool peak=true)
        {
            if (peak)
            {
                return analytics.IsResourceUsagePeak(resourceUsage);
            }
            return analytics.IsResourceUsageLowest(resourceUsage);
        }

        public List<TotalResourceUsageModel> ExtremeUsage(bool peak=true)
        {
            List<TotalResourceUsageModel> dailyUsages = this.Daily();
            List<TotalResourceUsageModel> peakUsages = new List<TotalResourceUsageModel>();

            foreach(TotalResourceUsageModel usage in dailyUsages)
            {
                if (this.IsUsageExtreme(usage, peak))
                {
                    peakUsages.Add(usage);
                }
            }
            return peakUsages;
        }


        public List<string> ExtremeUsageDuration(bool peak=true)
        {
            List<TotalResourceUsageModel> dailyUsages = this.Daily();
            List<TotalResourceUsageModel> peakUsages = new List<TotalResourceUsageModel>();
            int longestDuration = 1;
            for (int i=0; i< dailyUsages.Count-1; i++)
            {
                int nextPeakUsagePos = i+1;
                while (this.IsUsageExtreme(dailyUsages[nextPeakUsagePos], peak))
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
            foreach (TotalResourceUsageModel usage in peakUsages)
            {
                peakEpochs.Add(usage.epocTime.ToString());
            }
            return peakEpochs;
        }
    }
}