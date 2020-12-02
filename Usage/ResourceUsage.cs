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

        public float Current()
        {
            float performanceValue  = 0;
            PerformanceCounter performanceCounter = new PerformanceCounter();
            if (resourceType == ResourceUsageType.cpu)
            {
                performanceCounter.CategoryName = "Processor";
                performanceCounter.CounterName = "% Processor Time";
                performanceCounter.InstanceName = "_Total";

                performanceValue = performanceCounter.NextValue();
                System.Threading.Thread.Sleep(1000);
                performanceValue = performanceCounter.NextValue();
            } 
            else
            {
                performanceCounter.CategoryName = "Memory";
                performanceCounter.CounterName = "Available MBytes";
                performanceValue = performanceCounter.NextValue();
            }
            return performanceValue;
        }
    }   
}