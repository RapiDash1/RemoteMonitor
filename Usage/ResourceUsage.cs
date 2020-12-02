using System;
using System.Diagnostics;

namespace RemoteMonitor.Usage
{

    public enum ResourceUsageType
    {
        cpu,
        memory
    }
    public class ResourceUsage
    {

        public ResourceUsage(ResourceUsageType resourceType)
        {
            this.resourceType = resourceType;
        }
        public ResourceUsageType resourceType { get; set; }

        public float cpuUsage() 
        {
            PerformanceCounter performanceCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");

            performanceCounter.NextValue();
            System.Threading.Thread.Sleep(1000);
            return performanceCounter.NextValue();
        }

        public float memoryUsage()
        {
            PerformanceCounter performanceCounter = new PerformanceCounter("Memory", "Available MBytes");
            return performanceCounter.NextValue();
        }

        public float Current()
        {
            if (resourceType == ResourceUsageType.cpu) return this.cpuUsage();
            return this.memoryUsage();
        }
    }   
}