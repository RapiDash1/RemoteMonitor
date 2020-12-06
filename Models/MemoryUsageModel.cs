using System;

namespace RemoteMonitor.Models
{
    public class MemoryUsageModel : ResourceUsageModel
    {

        public MemoryUsageModel(int epocTime, float resourceUsage) : base(epocTime, resourceUsage)
        {
        }

        public override string ResourceTypeString()
        {
            return "MB";
        }
    }
}