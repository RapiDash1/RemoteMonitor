using System;

namespace RemoteMonitor.Models
{
    public class TotalResourceUsageModel
    {
        
        /// <summary>
        /// epoc time in seconds in int
        /// </summary>
        public int epocTime { get; set; }

        /// <summary>
        /// CPU usage in float
        /// </summary>
        public float cpuUsage { get; set; }

        /// <summary>
        /// Memory usage in float
        /// </summary>
        public float memoryAvailable { get; set; }

        public TotalResourceUsageModel(int epocTime, float cpuUsage, float memoryAvailable)
        {
            this.epocTime = epocTime;
            this.cpuUsage = cpuUsage;
            this.memoryAvailable = memoryAvailable;
        }

        /// <summary>
        /// String representation of this class
        /// </summary>
        /// <returns>
        /// A string representing information in this class
        /// </returns>
        public override string ToString()
        {
            return "EpocTime: " + this.epocTime + " || CpuUsage: " + 
                    this.cpuUsage + "% || MemoryAvailable: " + this.memoryAvailable +" MB";
        }
    }
}