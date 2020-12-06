using System;

namespace RemoteMonitor.Models
{
    public class CpuUsageModel : ResourceUsageModel
    {

        public CpuUsageModel(int epocTime, float resourceUsage) : base(epocTime, resourceUsage)
        {
        }

        public override string ResourceTypeString()
        {
            return "%";
        }
    }
}