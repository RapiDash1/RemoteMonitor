using System;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using RemoteMonitor.Models;

namespace RemoteMonitor.DbHandlers
{
    public class TotalResourceUsageDbHandler
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

        /// <summary>
        /// Get Resource usage
        /// </summary>
        /// <param name="query">Query string to get resource usage</param>
        /// <returns>
        /// The list of TotalResourceUsageModel
        /// </returns>
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
            return resourceUsages;
        }

        /// <summary>
        /// Query to retrieve daily usage from database
        /// </summary>
        /// <returns>
        /// The query string
        /// </returns>
        public string DailyUsageQuery()
        {
            return String.Format(@"SELECT EpocTime, CpuUsage, MemoryUsage FROM {0} WHERE EpocTime >= {1}", 
                                    this.TableName(), this.StartOfTheDayInEpochSeconds());
        }

        /// <summary>
        /// Get daily resource usage
        /// </summary>
        /// <returns>
        /// The list of ResourceUsageModel
        /// </returns>
        public List<TotalResourceUsageModel> GetDailyUsage()
        {
            List<TotalResourceUsageModel> resourceUsages = GetResourceUsage(this.DailyUsageQuery());
            return resourceUsages;
        }
    }
}