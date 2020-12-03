using System.Diagnostics;

namespace RemoteMonitor.Usage   
{
    public class CpuUsage : ResourceUsage
    {

        public float Current() 
        {
            PerformanceCounter performanceCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");

            performanceCounter.NextValue();
            System.Threading.Thread.Sleep(1000);
            return performanceCounter.NextValue();
        }
    }
}