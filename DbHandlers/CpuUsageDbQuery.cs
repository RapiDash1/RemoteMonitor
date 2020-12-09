using System;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using RemoteMonitor.Models;
using System.Linq;

namespace RemoteMonitor.DbHandlers
{
    public class CpuUsageDbQuery : ResourceUsageDbQuery
    {
        
        /// <summary>
        /// Query to retrieve daily usage from database
        /// </summary>
        /// <returns>
        /// The query string
        /// </returns>
        public override string DailyUsageQuery()
        {
            return String.Format(@"SELECT EpocTime, CpuUsage FROM {0} WHERE EpocTime >= {1}", 
                                    this.TableName(), this.StartOfTheDayInEpochSeconds());
        }

        /// <summary>
        /// Construct Usage model from database
        /// Get values from database and create a model for it
        /// </summary>
        /// <returns>
        /// A ResourceUsageModel
        /// </returns>
        public override ResourceUsageModel ConstructUsageModel(SqliteDataReader reader)
        {
            return new CpuUsageModel(reader.GetInt32(0), reader.GetFloat(1));
        }
    }
}