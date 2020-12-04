using System;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using RemoteMonitor.Models;

namespace RemoteMonitor.Analytics
{
    public class TotalResourceUsageDbHandler
    {

        public string databaseName()
        {
            return "ResourceDb";
        }

        public string databaseConnectionString()
        {
            return String.Format("Data Source={0}.db", this.databaseName());
        }

        public void SaveResourceUsage(string query)
        {
            List<TotalResourceUsageModel> resourceUsages = new List<TotalResourceUsageModel>{};
            using (var connection = new SqliteConnection(this.databaseConnectionString()))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = query;

                command.ExecuteReader();
            }
        }


        public List<TotalResourceUsageModel> GetResourceUsage(string query)
        {
            List<TotalResourceUsageModel> resourceUsages = new List<TotalResourceUsageModel>{};
            using (var connection = new SqliteConnection(this.databaseConnectionString()))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = query;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        resourceUsages.Add(new TotalResourceUsageModel(
                                Convert.ToUInt64(reader.GetString(0)), 
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