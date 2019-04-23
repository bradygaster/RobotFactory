using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Dashboard.Interfaces;

namespace MonitorBot
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        DashboardClient _client;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        #pragma warning disable CS4014, CS1998
        public override async Task StartAsync(CancellationToken stoppingToken)
        {
            _client = DashboardClient.Start("MonitorBot");
            base.StartAsync(stoppingToken);
        }
        
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                _client.SendStatus("Running");
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
