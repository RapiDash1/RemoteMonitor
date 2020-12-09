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
    /// Control Api behaviour for /totalresources path
    /// </summary>
    [Route("totalresources/")]
    [ApiController]
    public class TotalResourceController : ControllerBase
    {
        private TotalResourceUsageAnalytics analytics = new TotalResourceUsageAnalytics();

        /// <summary>
        /// Get current Total Resource usage
        /// </summary>
        /// <returns>
        /// The current Total Resource usage
        /// </returns>
        [HttpGet("current")]
        public string Current()
        {
            return analytics.Current();
        }

        /// <summary>
        /// Get daily Total Resource usage
        /// </summary>
        /// <returns>
        /// The list of TotalResourceUsageModel
        /// </returns>
        [HttpGet("daily")]
        public List<TotalResourceUsageModel> Daily()
        {
            TotalResourceUsageDbHandler usageDbHandler = new TotalResourceUsageDbHandler();
            return usageDbHandler.GetDailyUsage();
        }

        /// <summary>
        /// Get peak Total Resource usages
        /// Peak Total Resource usage is usage that is above either CPU or Memory peak threshold
        /// </summary>
        /// <returns>
        /// A list of TotalResourceUsageModel
        /// </returns>
        [HttpGet("peakusages")]
        public List<TotalResourceUsageModel> PeakUsages()
        {
            return this.ExtremeUsage(true);
        }

        /// <summary>
        /// Get lowest Total Resource usages
        /// Lowest Total Resource usage is usage that is above either CPU or Memory low threshold
        /// </summary>
        /// <returns>
        /// A list of TotalResourceUsageModel
        /// </returns>
        [HttpGet("lowestusages")]
        public List<TotalResourceUsageModel> LowestUsages()
        {
            return this.ExtremeUsage(false);
        }

        /// <summary>
        /// Get start and end times of the longest duration of peak Total Resource usage
        /// </summary>
        /// <returns>
        /// A list of strings containing the start and end times of longest peak Total Resource usage
        /// </returns>
        [HttpGet("longestpeakusage")]
        public List<string> longestPeakUsageDuraiton()
        {
            return this.ExtremeUsageDuration(true);
        }

        /// <summary>
        /// Get start and end times of the longest duration of lowest Total Resource usage
        /// </summary>
        /// <returns>
        /// A list of strings containing the start and end times of longest lowest Total Resource usage
        /// </returns>
        [HttpGet("longestlowestusage")]
        public List<string> longestLowestUsageDuraiton()
        {
            return this.ExtremeUsageDuration(false);
        }

        /// <summary>
        /// Check if usage is either peak or low
        /// </summary>
        /// <param name="cpuUsageValue">Total Resource usage value to check if it is extreme</param>
        /// <param name="peak">Should function check for peak or low usage?</param>
        /// <returns>
        /// A bool indicating whether usage is extreme, either low or peak
        /// </returns>
        public bool IsUsageExtreme(TotalResourceUsageModel resourceUsage, bool peak=true)
        {
            if (peak)
            {
                return analytics.IsResourceUsagePeak(resourceUsage);
            }
            return analytics.IsResourceUsageLowest(resourceUsage);
        }

        /// <summary>
        /// Get Peak or Low usage
        /// </summary>
        /// <param name="peak">Should function return peak or low usage?</param>
        /// <returns>
        /// A list of TotalResourceUsageModel which have extreme Total Resource usage
        /// </returns>
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

        /// <summary>
        /// Get maximum duration of Peak or Low usage
        /// </summary>
        /// <param name="peak">Should function return peak or low usage?</param>
        /// <returns>
        /// A list of strings containing the start and end times of longest peak Total Resource usage
        /// </returns>
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