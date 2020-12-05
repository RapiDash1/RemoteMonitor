using System;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using RemoteMonitor.Models;

namespace RemoteMonitor.DbHandlers
{
    public class TotalResourceUsageDbHandler
    {

        public string databaseName()
        {
            return "ResourceDb";
        }

        public string tableName()
        {
            return "ResourceUsage";
        }

        public string databaseConnectionString()
        {
            return String.Format("Data Source={0}.db", this.databaseName());
        }

        public void SaveResourceUsage(TotalResourceUsageModel usageModel)
        {
            List<TotalResourceUsageModel> resourceUsages = new List<TotalResourceUsageModel>{};
            using (var connection = new SqliteConnection(this.databaseConnectionString()))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = String.Format(@"INSERT INTO {0} (EpocTime, 
                        CpuUsage, MemoryUsage) VALUES ({1}, {2}, {3});", this.tableName(),
                                usageModel.epocTime, usageModel.cpuUsage, usageModel.memoryAvailable);

                command.ExecuteReader();
            }
        }


        public List<TotalResourceUsageModel> GetResourceUsage()
        {
            List<TotalResourceUsageModel> resourceUsages = new List<TotalResourceUsageModel>{};
            using (var connection = new SqliteConnection(this.databaseConnectionString()))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = String.Format(@"SELECT * from {0}", this.tableName());

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
    }
}