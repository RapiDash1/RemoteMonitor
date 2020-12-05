using System;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using RemoteMonitor.Models;
using System.Linq;

namespace RemoteMonitor.DbHandlers
{
    public class MemoryUsageDbQuery : ResourceUsageDbQuery
    {
        
        public override ResourceUsageModel ConstructUsageModel(SqliteDataReader reader)
        {
            return new MemoryUsageModel(Convert.ToUInt64(reader.GetString(0)), reader.GetFloat(1));
        }

        public List<MemoryUsageModel> GetDailyUsage()
        {
            List<ResourceUsageModel> cpuUsages = GetResourceUsage(String.Format(
                        @"SELECT EpocTime, MemoryUsage FROM {0} WHERE EpocTime >= {1}", 
                                    base.TableName(), base.StartOfTheDayInEpochSeconds()));
            
            return cpuUsages.Cast<MemoryUsageModel>().ToList();
        }
    }
}