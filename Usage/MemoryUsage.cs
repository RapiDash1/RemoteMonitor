using System.Diagnostics;

namespace RemoteMonitor.Usage   
{
    public class MemoryUsage : ResourceUsage
    {

        public float Current() 
        {
            PerformanceCounter performanceCounter = new PerformanceCounter("Memory", "Available MBytes");
            return performanceCounter.NextValue();
        }
    }
}