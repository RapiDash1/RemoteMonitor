using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using RemoteMonitor.Usage;
using RemoteMonitor.Models;
using RemoteMonitor.DbHandlers;

namespace RemoteMonitor.Controller
{
    [Route("cpu/")]
    [ApiController]
    public class CpuController : ControllerBase
    {
        [HttpGet("current")]
        public float Current()
        {
            CpuUsage cpuUsage = new CpuUsage();
            return cpuUsage.Current();
        }

        [HttpGet("daily")]
        public List<CpuUsageModel> Daily()
        {
            CpuUsageDbQuery cpuUsageDbQuery = new CpuUsageDbQuery();
            return cpuUsageDbQuery.DailyUsage();
        }
    }
}