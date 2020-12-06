using System;

namespace RemoteMonitor.Models
{
    public class ResourceUsageModel
    {
        
        public int epocTime { get; set; }

        public float resourceUsage { get; set; }

        public ResourceUsageModel(int epocTime, float resourceUsage)
        {
            this.epocTime = epocTime;
            this.resourceUsage = resourceUsage;
        }

        public virtual string ResourceTypeString()
        {
            return "";
        }

        public override string ToString()
        {
            return "EpocTime: " + this.epocTime + " resource usage: " + 
                            resourceUsage + " " + this.ResourceTypeString();
        }
    }
}