using System.Diagnostics;

namespace RemoteMonitor.Usage   
{
    public class MemoryUsage : ResourceUsage
    {
        
        /// <summary>
        /// Get Current Total Resource Usage
        /// </summary>
        /// <returns>
        /// A floating point number containing current resource usage
        /// </returns>
        public float Current() 
        {
            PerformanceCounter performanceCounter = new PerformanceCounter("Memory", "Available MBytes");
            return performanceCounter.NextValue();
        }
    }
}