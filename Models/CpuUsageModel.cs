using System;

namespace RemoteMonitor.Models
{
    public class CpuUsageModel : ResourceUsageModel
    {

        public float cpuUsage { get; set; }

        public CpuUsageModel(UInt64 epocTime, float cpuUsage) : base(epocTime)
        {
            this.cpuUsage = cpuUsage;
        }

        public override string ToString()
        {
            return "EpocTime: " + this.epocTime + " || CpuUsage: " + this.cpuUsage + "%";
        }
    }
}