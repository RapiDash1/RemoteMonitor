using System;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using RemoteMonitor.Models;

namespace RemoteMonitor.DbHandlers
{
    public class ResourceUsageDbQuery
    {
        
        /// <summary>
        /// Database Name
        /// </summary>
        /// <returns>
        /// A string containing the database name
        /// </returns>
        public string DatabaseName()
        {
            return "ResourceDb";
        }

        /// <summary>
        /// Table Name
        /// </summary>
        /// <returns>
        /// A string containing the table name
        /// </returns>
        public string TableName()
        {
            return "ResourceUsage";
        }

        /// <summary>
        /// A string in the format to connect to the database
        /// with name <code>DatabaseName()</code>
        /// </summary>
        /// <returns>
        /// A string in the specified format to connect to database
        /// </returns>
        public string DatabaseConnectionString()
        {
            return String.Format("Data Source={0}.db", this.DatabaseName());
        }

        /// <summary>
        /// Get the time in epoch seconds of the start of the day
        /// </summary>
        /// <returns>
        /// An int representing the start of the day in epoch seconds
        /// </returns>
        public int StartOfTheDayInEpochSeconds()
        {
            TimeSpan t = DateTime.Today.Date - new DateTime(1970, 1, 1);
            return (int)t.TotalSeconds;
        }

        /// <summary>
        /// Construct Usage model from database
        /// Get values from database and create a model for it
        /// </summary>
        /// <returns>
        /// A ResourceUsageModel
        /// </returns>
        public virtual ResourceUsageModel ConstructUsageModel(SqliteDataReader reader)
        {
            return new ResourceUsageModel(reader.GetInt32(0), reader.GetFloat(1));
        }

        /// <summary>
        /// Get Resource usage
        /// </summary>
        /// <param name="query">Query string to get resource usage</param>
        /// <returns>
        /// The list of ResourceUsageModel
        /// </returns>
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
            return resourceUsages;
        }

        /// <summary>
        /// Query to retrieve daily usage from database
        /// </summary>
        /// <returns>
        /// The query string
        /// </returns>
        public virtual string DailyUsageQuery()
        {
            return String.Format(@"SELECT EpocTime, CpuUsage FROM {0} WHERE EpocTime >= {1}", 
                                    this.TableName(), this.StartOfTheDayInEpochSeconds());
        }

        /// <summary>
        /// Get daily resource usage
        /// </summary>
        /// <returns>
        /// The list of ResourceUsageModel
        /// </returns>
        public List<ResourceUsageModel> GetDailyUsage()
        {
            List<ResourceUsageModel> resourceUsages = GetResourceUsage(this.DailyUsageQuery());
            return resourceUsages;
        }
    }
}