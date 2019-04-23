using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.SignalR.Client;

namespace RepairBot
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private HubConnection Connection { get; set; }

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        #pragma warning disable CS4014, CS1998
        public override async Task StartAsync(CancellationToken stoppingToken)
        {
            try
            {
                Connection = new HubConnectionBuilder()
                    .WithUrl("http://dashboard.robotfactory/heartbeat")
                    .Build();

                await Connection.StartAsync();

                _logger.LogInformation("Dashboard client connected");
            }
            catch (System.Exception ex)
            {
                _logger.LogError("Error during connection", ex);
            }
            
            base.StartAsync(stoppingToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                try
                {
                    await Connection.InvokeAsync("SendHeartbeat", 
                        "Running",
                        "RepairBot");
                }
                catch (System.Exception ex)
                {
                    _logger.LogError("Error sending heartbeat", ex);
                }

                await Task.Delay(2000, stoppingToken);
            }
        }
    }
}
