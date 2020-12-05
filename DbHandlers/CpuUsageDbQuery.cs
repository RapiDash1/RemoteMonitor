using System;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using RemoteMonitor.Models;

namespace RemoteMonitor.DbHandlers
{
    public class CpuUsageDbQuery : ResourceUsageDbQuery
    {

        public override ResourceUsageModel ConstructUsageModel(SqliteDataReader reader)
        {
            return new CpuUsageModel(Convert.ToUInt64(reader.GetString(0)), reader.GetFloat(1));
        }

        public List<ResourceUsageModel> GetCpuUsage()
        {
            return GetResourceUsage(String.Format(@"SELECT EpocTime, CpuUsage FROM {0}", base.tableName()));
        }
    }
}