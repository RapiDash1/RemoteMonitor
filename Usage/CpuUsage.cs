using System.Diagnostics;

namespace RemoteMonitor.Usage   
{
    public class CpuUsage : ResourceUsage
    {
        
        /// <summary>
        /// Get Current Total Resource Usage
        /// </summary>
        /// <returns>
        /// A floating point number containing current resource usage
        /// </returns>
        public float Current() 
        {
            PerformanceCounter performanceCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");

            performanceCounter.NextValue();
            // Sleep for 1s to sync
            System.Threading.Thread.Sleep(1000);
            return performanceCounter.NextValue();
        }
    }
}