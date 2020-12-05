using System;

namespace RemoteMonitor.Models
{
    public class MemoryUsageModel : ResourceUsageModel
    {

        public float memoryAvailable { get; set; }

        public MemoryUsageModel(UInt64 epocTime, float memoryAvailable) : base(epocTime)
        {
            this.memoryAvailable = memoryAvailable;
        }

        public override string ToString()
        {
            return "EpocTime: " + this.epocTime + " || MemoryAvailable: " + this.memoryAvailable + " MB";
        }
    }
}