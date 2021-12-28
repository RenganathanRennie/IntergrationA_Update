using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using System.Threading;
using Microsoft.Extensions.Logging;

namespace IntergrationA.Services.TaskService 
{
    public class RunAsyncTask : BackgroundService
    {
        public readonly ILogger<RunAsyncTask>_log;
        public RunAsyncTask(ILogger<RunAsyncTask>_log)
        {
            this._log=_log;
        }        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
             while(!stoppingToken.IsCancellationRequested)
            {
                 _log.LogInformation("Loop ran at "+DateTime.Now.ToString());
                await Task.Delay(2000);
            }
        }
    }
}