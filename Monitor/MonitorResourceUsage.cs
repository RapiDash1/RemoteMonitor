using RemoteMonitor.DbHandlers;
using RemoteMonitor.Models;
using RemoteMonitor.Usage;
using System.Collections.Generic;
using System;

namespace RemoteMonitor.Monitor
{
    public class MonitorResourceUsage
    {

        private TotalResourceUsageDbHandler dbHandler = new TotalResourceUsageDbHandler();

        private int SecondsSinceEpoh()
        {
            TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
            return (int)t.TotalSeconds;
        }

        public void SaveUsage()
        {
            CpuUsage cpuUsage = new CpuUsage();
            MemoryUsage memoryAvailable = new MemoryUsage();
            while (true)
            {   
                float cpuUsageValue = cpuUsage.Current();
                float memoryUsageValue = memoryAvailable.Current();
                Console.WriteLine(Convert.ToString(cpuUsageValue) + "% || " + Convert.ToString(memoryUsageValue) + " MB");
                this.dbHandler.SaveResourceUsage(new TotalResourceUsageModel(this.SecondsSinceEpoh(), cpuUsageValue, memoryUsageValue));
            }
        }


        public List<TotalResourceUsageModel> GetResourceUsage()
        {
            return this.dbHandler.GetResourceUsage();
        }
    }
}