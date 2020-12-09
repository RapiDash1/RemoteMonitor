using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using RemoteMonitor.Usage;
using RemoteMonitor.Models;
using RemoteMonitor.DbHandlers;
using RemoteMonitor.Analytics;

namespace RemoteMonitor.Controller
{
    /// <summary>
    /// Control Api behaviour for /memory path
    /// </summary>
    [Route("memory/")]
    [ApiController]
    public class MemoryController : ControllerBase
    {
        private MemoryUsageAnalytics analytics = new MemoryUsageAnalytics();
        
        /// <summary>
        /// Get current Memory usage
        /// </summary>
        /// <returns>
        /// The current Memory usage percent in the form of a floating point number
        /// </returns>
        [HttpGet("current")]
        public float Current()
        {
            return analytics.Current();
        }

        /// <summary>
        /// Get daily Memory usage
        /// </summary>
        /// <returns>
        /// A list of MemoryUsageModel
        /// </returns>
        [HttpGet("daily")]
        public List<MemoryUsageModel> Daily()
        {
            return analytics.Daily();
        }

        /// <summary>
        /// Get peak Memory usages
        /// Peak Memory usage is usage that is above <code>analytics.peakUsageThreshold<code>
        /// </summary>
        /// <returns>
        /// A list of MemoryUsageModel
        /// </returns>
        [HttpGet("peakusages")]
        public List<MemoryUsageModel> PeakUsages()
        {
            return this.ExtremeUsage(true);
        }

        /// <summary>
        /// Get lowest Memory usages
        /// Lowest Memory usage is usage that is below <code>analytics.lowestUsageThreshold<code>
        /// </summary>
        /// <returns>
        /// A list of MemoryUsageModel
        /// </returns>
        [HttpGet("lowestusages")]
        public List<MemoryUsageModel> LowestUsages()
        {
            return this.ExtremeUsage(false);
        }

        /// <summary>
        /// Get start and end times of the longest duration of peak Memory usage
        /// </summary>
        /// <returns>
        /// A list of strings containing the start and end times of longest peak Memory usage
        /// </returns>
        [HttpGet("longestpeakusage")]
        public List<string> LongestPeakUsageDuraiton()
        {
            return this.ExtremeUsageDuration(true);
        }

        /// <summary>
        /// Get start and end times of the longest duration of lowest Memory usage
        /// </summary>
        /// <returns>
        /// A list of strings containing the start and end times of longest lowest Memory usage
        /// </returns>
        [HttpGet("longestlowestusage")]
        public List<string> LongestLowestUsageDuraiton()
        {
            return this.ExtremeUsageDuration(false);
        }

        /// <summary>
        /// Check if usage is either peak or low
        /// </summary>
        /// <param name="cpuUsageValue">Memory usage value to check if it is extreme</param>
        /// <param name="peak">Should function check for peak or low usage?</param>
        /// <returns>
        /// A bool indicating whether usage is extreme, either low or peak
        /// </returns>
        public bool IsUsageExtreme(float memoryUsageValue, bool peak=true)
        {
            if (peak)
            {
                return analytics.IsResourceUsagePeak(memoryUsageValue);
            }
            return analytics.IsResourceUsageLowest(memoryUsageValue);
        }

        /// <summary>
        /// Get Peak or Low usage
        /// </summary>
        /// <param name="peak">Should function return peak or low usage?</param>
        /// <returns>
        /// A list of MemoryUsageModel which have extreme Memory usage
        /// </returns>
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

        /// <summary>
        /// Get maximum duration of Peak or Low usage
        /// </summary>
        /// <param name="peak">Should function return peak or low usage?</param>
        /// <returns>
        /// A list of strings containing the start and end times of longest peak Memory usage
        /// </returns>
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