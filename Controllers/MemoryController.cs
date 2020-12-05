using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using RemoteMonitor.Usage;
using RemoteMonitor.Models;
using RemoteMonitor.DbHandlers;

namespace RemoteMonitor.Controller
{
    [Route("memory/")]
    [ApiController]
    public class MemoryController : ControllerBase
    {
        [HttpGet("current")]
        public float Current()
        {
            MemoryUsage cpuUsage = new MemoryUsage();
            return cpuUsage.Current();
        }

        [HttpGet("daily")]
        public List<MemoryUsageModel> Daily()
        {
            MemoryUsageDbQuery memoryUsageDbQuery = new MemoryUsageDbQuery();
            return memoryUsageDbQuery.GetDailyUsage();
        }
    }
}