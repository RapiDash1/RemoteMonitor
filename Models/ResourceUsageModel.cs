using System;

namespace RemoteMonitor.Models
{
    public class ResourceUsageModel
    {
        
        /// <summary>
        /// epoc time in seconds in int
        /// </summary>
        public int epocTime { get; set; }

        /// <summary>
        /// CPU usage in float
        /// </summary>
        public float resourceUsage { get; set; }

        public ResourceUsageModel(int epocTime, float resourceUsage)
        {
            this.epocTime = epocTime;
            this.resourceUsage = resourceUsage;
        }
        
        /// <summary>
        /// String representation of this class
        /// </summary> 
        /// <returns>
        /// A string the type of the resource to suffix
        /// </returns>     
        public virtual string ResourceTypeString()
        {
            return "";
        }

        /// <summary>
        /// String representation of this class
        /// </summary>
        /// <returns>
        /// A string representing information in this class
        /// </returns>
        public override string ToString()
        {
            return "EpocTime: " + this.epocTime + " resource usage: " + 
                            resourceUsage + " " + this.ResourceTypeString();
        }
    }
}