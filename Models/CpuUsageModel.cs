using System;

namespace RemoteMonitor.Models
{
    public class CpuUsageModel : ResourceUsageModel
    {

        public CpuUsageModel(int epocTime, float resourceUsage) : base(epocTime, resourceUsage)
        {
        }

        /// <summary>
        /// String representation of this class
        /// </summary> 
        /// <returns>
        /// A string the type of the resource to suffix
        /// </returns> 
        public override string ResourceTypeString()
        {
            return "%";
        }
    }
}