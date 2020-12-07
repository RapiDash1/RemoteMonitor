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
    [Route("cpu/")]
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
        public List<CpuUsageModel> Daily()
        {
            CpuUsageDbQuery cpuUsageDbQuery = new CpuUsageDbQuery();
            return cpuUsageDbQuery.GetDailyUsage().Cast<CpuUsageModel>().ToList();
        }

        [HttpGet("peakusages")]
        public List<CpuUsageModel> PeakUsages()
        {
            return this.ExtremeUsage(true);
        }

        [HttpGet("lowestusages")]
        public List<CpuUsageModel> LowestUsages()
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


        public bool IsUsageExtreme(float cpuUsageValue, bool peak=true)
        {
            // if (peak)
            // {
            //     return analytics.IsResourceUsagePeak(cpuUsageValue);
            // }
            // return analytics.IsResourceUsageLowest(cpuUsageValue);
            return true;
        }

        public List<CpuUsageModel> ExtremeUsage(bool peak=true)
        {
            List<CpuUsageModel> dailyUsages = this.Daily();
            List<CpuUsageModel> peakUsages = new List<CpuUsageModel>();

            foreach(CpuUsageModel usage in dailyUsages)
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
            List<CpuUsageModel> dailyUsages = this.Daily();
            List<CpuUsageModel> peakUsages = new List<CpuUsageModel>();
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
            foreach (CpuUsageModel usage in peakUsages)
            {
                peakEpochs.Add(usage.epocTime.ToString());
            }
            return peakEpochs;
        }
    }
}