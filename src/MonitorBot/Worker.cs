using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.SignalR.Client;

namespace MonitorBot
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private HubConnection Connection { get; set; }
        public int ChargeLevel { get; set; }

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
            ChargeLevel = 100;
        }

        #pragma warning disable CS4014, CS1998
        public override async Task StartAsync(CancellationToken stoppingToken)
        {
            try
            {
                Connection = new HubConnectionBuilder()
                    //.WithUrl("http://dashboard.robotfactory/heartbeat")
                    .WithUrl("https://localhost:5001/heartbeat")
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
                _logger.LogInformation("Worker running with {1} at: {time}", DateTimeOffset.Now, ChargeLevel);
                ChargeLevel -= 10;

                try
                {
                    await Connection.InvokeAsync("SendHeartbeat", 
                        "Running",
                        "MonitorBot",
                        ChargeLevel);
                }
                catch (System.Exception ex)
                {
                    _logger.LogError("Error sending heartbeat", ex);
                }

                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}
