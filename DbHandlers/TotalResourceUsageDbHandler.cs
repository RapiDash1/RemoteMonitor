using System;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using RemoteMonitor.Models;

namespace RemoteMonitor.DbHandlers
{
    public class TotalResourceUsageDbHandler
    {

        public string DatabaseName()
        {
            return "ResourceDb";
        }

        public string TableName()
        {
            return "ResourceUsage";
        }

        public string DatabaseConnectionString()
        {
            return String.Format("Data Source={0}.db", this.DatabaseName());
        }

        public int StartOfTheDayInEpochSeconds()
        {
            TimeSpan t = DateTime.Today.Date - new DateTime(1970, 1, 1);
            return (int)t.TotalSeconds;
        }

        public void SaveResourceUsage(TotalResourceUsageModel usageModel)
        {
            List<TotalResourceUsageModel> resourceUsages = new List<TotalResourceUsageModel>{};
            using (var connection = new SqliteConnection(this.DatabaseConnectionString()))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = String.Format(@"INSERT INTO {0} (EpocTime, 
                        CpuUsage, MemoryUsage) VALUES ({1}, {2}, {3});", this.TableName(),
                                usageModel.epocTime, usageModel.cpuUsage, usageModel.memoryAvailable);

                command.ExecuteReader();
            }
        }


        public List<TotalResourceUsageModel> GetResourceUsage(string query)
        {
            List<TotalResourceUsageModel> resourceUsages = new List<TotalResourceUsageModel>{};
            using (var connection = new SqliteConnection(this.DatabaseConnectionString()))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = String.Format(query);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        resourceUsages.Add(new TotalResourceUsageModel(
                                Convert.ToInt32(reader.GetString(0)), 
                                        reader.GetFloat(1), reader.GetFloat(2)));
                    }
                }
            }
            foreach (TotalResourceUsageModel usage in resourceUsages)    
            {
                Console.WriteLine(usage.ToString());
            }
            return resourceUsages;
        }

        public string DailyUsageQuery()
        {
            return String.Format(@"SELECT EpocTime, CpuUsage FROM {0} WHERE EpocTime >= {1}", 
                                    this.TableName(), this.StartOfTheDayInEpochSeconds());
        }

        public List<TotalResourceUsageModel> GetDailyUsage()
        {
            List<TotalResourceUsageModel> resourceUsages = GetResourceUsage(this.DailyUsageQuery());
            return resourceUsages;
        }
    }
}