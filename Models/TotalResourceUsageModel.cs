using System;

namespace RemoteMonitor.Models
{
    public class TotalResourceUsageModel
    {

        public TotalResourceUsageModel(int epocTime, float cpuUsage, float memoryAvailable)
        {
            this.epocTime = epocTime;
            this.cpuUsage = cpuUsage;
            this.memoryAvailable = memoryAvailable;
        }
        
        public int epocTime { get; set; }

        public float cpuUsage { get; set; }

        public float memoryAvailable { get; set; }

        public override string ToString()
        {
            return "EpocTime: " + this.epocTime + " || CpuUsage: " + 
                    this.cpuUsage + "% || MemoryAvailable: " + this.memoryAvailable +" MB";
        }
    }
}