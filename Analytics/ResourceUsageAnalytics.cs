using System;
using RemoteMonitor.Usage;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using RemoteMonitor.Models;

namespace RemoteMonitor.Analytics
{   
    class ResourceUsageAnalytics
    {
        public float peakUsageThreshold { get; set; } = 90;
        public float lowestUsageThreshold { get; set; } = 5;

        protected ResourceUsage resourceUsage;

        public bool IsResourceUsagePeak()
        {
            return resourceUsage.Current() >= peakUsageThreshold;
        }

        public bool IsResourceUsageLowest()
        {
            return resourceUsage.Current() <= lowestUsageThreshold;
        }

        public void SaveResourceUsage()
        {
            // Save info to db
        }


        public List<ResourceUsageModel> GetResourceUsage(string query)
        {
            List<ResourceUsageModel> resourceUsages = new List<ResourceUsageModel>{};
            using (var connection = new SqliteConnection("Data Source=ResourceDb.db"))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText =
                query;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        resourceUsages.Add(new ResourceUsageModel(
                                Convert.ToUInt64(reader.GetString(0)), 
                                        reader.GetFloat(1), reader.GetFloat(2)));
                    }
                }
            }
            foreach (ResourceUsageModel usage in resourceUsages)    
            {
                Console.WriteLine(usage.ToString());
            }
            return resourceUsages;
        }


    }
}