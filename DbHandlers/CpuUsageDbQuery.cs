using System;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using RemoteMonitor.Models;

namespace RemoteMonitor.Analytics
{
    public class CpuUsageDbQuery : ResourceUsageDbQuery
    {
        
        public override ResourceUsageModel constructUsageModel(SqliteDataReader reader)
        {
            return new CpuUsageModel(Convert.ToUInt64(reader.GetString(0)), reader.GetFloat(1));
        }
    }
}