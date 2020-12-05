using System;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using RemoteMonitor.Models;

namespace RemoteMonitor.DbHandlers
{
    public class ResourceUsageDbQuery
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


        public virtual ResourceUsageModel ConstructUsageModel(SqliteDataReader reader)
        {
            return new ResourceUsageModel(Convert.ToUInt64(reader.GetString(0)));
        }


        public List<ResourceUsageModel> GetResourceUsage(string query)
        {
            List<ResourceUsageModel> resourceUsages = new List<ResourceUsageModel>{};
            using (var connection = new SqliteConnection(this.DatabaseConnectionString()))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = query;
                Console.WriteLine(query);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        resourceUsages.Add(ConstructUsageModel(reader));
                    }
                }
            }
            // foreach (ResourceUsageModel usage in resourceUsages)    
            // {
            //     Console.WriteLine(usage.ToString());
            // }
            return resourceUsages;
        }
    }
}