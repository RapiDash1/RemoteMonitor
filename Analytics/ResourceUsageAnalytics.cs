using RemoteMonitor.Usage;
using RemoteMonitor.Models;
using RemoteMonitor.DbHandlers;
using System.Collections.Generic;

namespace RemoteMonitor.Analytics
{   
    class ResourceUsageAnalytics
    {
        /// <summary>
        /// Threshold for determining peak usage
        /// </summary>
        public float peakUsageThreshold { get; set; } = 90;

        /// <summary>
        /// Threshold for determining low usage
        /// </summary>
        public float lowestUsageThreshold { get; set; } = 5;

        protected ResourceUsage resourceUsage;

        /// <summary>
        /// Check if resource usage is peak
        /// </summary>
        /// <param name="usage">usage for which peak-check should be made</param>
        /// <returns>
        /// A bool indicating whether resource usage is peak
        /// </returns>
        public virtual bool IsResourceUsagePeak(float resourceUsage)
        {
            return resourceUsage >= peakUsageThreshold;
        }

        /// <summary>
        /// Check if resource usage is low
        /// </summary>
        /// <param name="usage">usage for which low-check should be made</param>
        /// <returns>
        /// A bool indicating whether resource usage is low
        /// </returns>
        public virtual bool IsResourceUsageLowest(float resourceUsage)
        {
            return resourceUsage <= lowestUsageThreshold;
        }

        /// <summary>
        /// Get Current Total Resource Usage
        /// </summary>
        /// <returns>
        /// A floating point number containing current resource usage
        /// </returns>
        public float Current()
        {
            return resourceUsage.Current();
        }
    }
}