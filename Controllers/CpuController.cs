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
            return cpuUsageDbQuery.GetDailyUsage().Cast<CpuUsageModel>().ToList();
        }

        [HttpGet("peakusages")]
        public List<CpuUsageModel> PeakUsages()
        {
            List<CpuUsageModel> dailyUsages = this.Daily();
            List<CpuUsageModel> peakUsages = new List<CpuUsageModel>();

            foreach(CpuUsageModel usage in dailyUsages)
            {
                if (analytics.IsResourceUsagePeak(usage.resourceUsage))
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
            List<CpuUsageModel> dailyUsages = this.Daily();
            List<CpuUsageModel> peakUsages = new List<CpuUsageModel>();
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
            foreach (CpuUsageModel usage in peakUsages)
            {
                peakEpochs.Add(usage.epocTime.ToString());
            }
            return peakEpochs;
        }
    }
}