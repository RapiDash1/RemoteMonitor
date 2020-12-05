using System;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using RemoteMonitor.Models;

namespace RemoteMonitor.DbHandlers
{
    public class MemoryUsageDbQuery : ResourceUsageDbQuery
    {
        
        public override ResourceUsageModel ConstructUsageModel(SqliteDataReader reader)
        {
            return new MemoryUsageModel(Convert.ToUInt64(reader.GetString(0)), reader.GetFloat(1));
        }

        public List<ResourceUsageModel> GetMemoryUsage()
        {
            return GetResourceUsage(String.Format(@"SELECT EpocTime, MemoryUsage FROM {0}", base.tableName()));
        }
    }
}