using System;

namespace RemoteMonitor.Models
{
    public class MemoryUsageModel : ResourceUsageModel
    {

        public float memoryUsage { get; set; }

        public MemoryUsageModel(UInt64 epocTime, float memoryUsage) : base(epocTime)
        {
            this.memoryUsage = memoryUsage;
        }

        public override string ToString()
        {
            return "EpocTime: " + this.epocTime + " || CpuUsage: " + this.memoryUsage + "%";
        }
    }
}