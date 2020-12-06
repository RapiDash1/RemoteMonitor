using System;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using RemoteMonitor.Models;
using System.Linq;

namespace RemoteMonitor.DbHandlers
{
    public class CpuUsageDbQuery : ResourceUsageDbQuery
    {

        public override string DailyUsageQuery()
        {
            return String.Format(@"SELECT EpocTime, CpuUsage FROM {0} WHERE EpocTime >= {1}", 
                                    this.TableName(), this.StartOfTheDayInEpochSeconds());
        }


        public override ResourceUsageModel ConstructUsageModel(SqliteDataReader reader)
        {
            return new CpuUsageModel(reader.GetInt32(0), reader.GetFloat(1));
        }
    }
}