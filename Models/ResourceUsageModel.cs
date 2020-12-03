using System;

namespace RemoteMonitor.Models
{
    public class ResourceUsageModel
    {

        public ResourceUsageModel(UInt64 epocTime, float cpuUsage, float memoryUsage)
        {
            this.epocTime = epocTime;
            this.cpuUsage = cpuUsage;
            this.memoryUsage = memoryUsage;
        }
        
        public UInt64 epocTime { get; set; }

        public float cpuUsage { get; set; }

        public float memoryUsage { get; set; }

        public override string ToString()
        {
            return "EpocTime: " + this.epocTime + " || CpuUsage: " + 
                    this.cpuUsage + "% || MemoryUsage: " + this.memoryUsage +" MB";
        }
    }
}