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
    /// <summary>
    /// Control Api behaviour for /cpu path
    /// </summary>
    [Route("cpu/")]
    [ApiController]
    public class CpuController : ControllerBase
    {
        private CpuUsageAnalytics analytics = new CpuUsageAnalytics();

        /// <summary>
        /// Get current CPU usage
        /// </summary>
        /// <returns>
        /// The current CPU usage percent in the form of a floating point number
        /// </returns>
        [HttpGet("current")]
        public float Current()
        {
            CpuUsage cpuUsage = new CpuUsage();
            return cpuUsage.Current();
        }

        /// <summary>
        /// Get daily CPU usage
        /// </summary>
        /// <returns>
        /// A list of CpuUsageModel
        /// </returns>
        [HttpGet("daily")]
        public List<CpuUsageModel> Daily()
        {
            CpuUsageDbQuery cpuUsageDbQuery = new CpuUsageDbQuery();
            return cpuUsageDbQuery.GetDailyUsage().Cast<CpuUsageModel>().ToList();
        }

        /// <summary>
        /// Get peak CPU usages
        /// Peak CPU usage is usage that is above <code>analytics.peakUsageThreshold<code>
        /// </summary>
        /// <returns>
        /// A list of CpuUsageModel
        /// </returns>
        [HttpGet("peakusages")]
        public List<CpuUsageModel> PeakUsages()
        {
            return this.ExtremeUsages(true);
        }

        /// <summary>
        /// Get lowest CPU usages
        /// Lowest CPU usage is usage that is below <code>analytics.lowestUsageThreshold<code>
        /// </summary>
        /// <returns>
        /// A list of CpuUsageModel
        /// </returns>
        [HttpGet("lowestusages")]
        public List<CpuUsageModel> LowestUsages()
        {
            return this.ExtremeUsages(false);
        }

        /// <summary>
        /// Get start and end times of the longest duration of peak CPU usage
        /// </summary>
        /// <returns>
        /// A list of strings containing the start and end times of longest peak CPU usage
        /// </returns>
        [HttpGet("longestpeakusage")]
        public List<string> longestPeakUsageDuraiton()
        {
            return this.ExtremeUsageDuration(true);
        }

        /// <summary>
        /// Get start and end times of the longest duration of lowest CPU usage
        /// </summary>
        /// <returns>
        /// A list of strings containing the start and end times of longest lowest CPU usage
        /// </returns>
        [HttpGet("longestlowestusage")]
        public List<string> longestLowestUsageDuraiton()
        {
            return this.ExtremeUsageDuration(false);
        }

        /// <summary>
        /// Check if usage is either peak or low
        /// </summary>
        /// <param name="cpuUsageValue">CPU usage value to check if it is extreme</param>
        /// <param name="peak">Should function check for peak or low usage?</param>
        /// <returns>
        /// A bool indicating whether usage is extreme, either low or peak
        /// </returns>
        public bool IsUsageExtreme(float cpuUsageValue, bool peak=true)
        {
            if (peak)
            {
                return analytics.IsResourceUsagePeak(cpuUsageValue);
            }
            return analytics.IsResourceUsageLowest(cpuUsageValue);
        }

        /// <summary>
        /// Get Peak or Low usage
        /// </summary>
        /// <param name="peak">Should function return peak or low usage?</param>
        /// <returns>
        /// A list of CpuUsageModel which have extreme CPU usage
        /// </returns>
        public List<CpuUsageModel> ExtremeUsages(bool peak=true)
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

        /// <summary>
        /// Get maximum duration of Peak or Low usage
        /// </summary>
        /// <param name="peak">Should function return peak or low usage?</param>
        /// <returns>
        /// A list of strings containing the start and end times of longest peak CPU usage
        /// </returns>
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