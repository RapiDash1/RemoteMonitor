using System;

namespace RemoteMonitor.Models
{
    public class ResourceUsageModel
    {

        public ResourceUsageModel(UInt64 epocTime)
        {
            this.epocTime = epocTime;
        }
        
        public UInt64 epocTime { get; set; }

        public override string ToString()
        {
            return "EpocTime: " + this.epocTime;
        }
    }
}