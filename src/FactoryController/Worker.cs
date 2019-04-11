using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;

namespace FactoryController
{
    public class Worker : BackgroundService
    {
        private const string DASHBOARD_QUEUE_CONNECTION_STRING = "DASHBOARD_QUEUE_CONNECTION_STRING";
        private const string DASHBOARD_QUEUE = "dashboardheartbeat";

        private readonly ILogger<Worker> _logger;
        HttpClient _httpClient;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Factory running at: {time}", DateTimeOffset.Now);
                await Task.Delay(5000, stoppingToken);
                
                try
                {
                    if(_httpClient == null)
                        _httpClient = new HttpClient();
                        
                    var result = await _httpClient.GetAsync("http://dashboard");
                    _logger.LogInformation($"HTTP response from dashboard: {result.StatusCode}");

                    await EnqueueDashboardPing();
                }
                catch(Exception ex)
                {
                    _logger.LogError(ex, "Error during dashboard test");
                }
            }
        }

        private async Task EnqueueDashboardPing()
        {
            _logger.LogInformation("Sending dashboard heartbeat at: {time}", DateTimeOffset.Now);

            CloudStorageAccount storageAccount = null;

            var success = CloudStorageAccount.TryParse(
                Environment.GetEnvironmentVariable(DASHBOARD_QUEUE_CONNECTION_STRING),
                out storageAccount
            );

            if(success)
            {
                var queueClient = storageAccount.CreateCloudQueueClient();
                var queue = queueClient.GetQueueReference(DASHBOARD_QUEUE);
                await queue.CreateIfNotExistsAsync();
                await queue.AddMessageAsync(new CloudQueueMessage("FactoryControllerHeartbeat"));
            }

            _logger.LogInformation("Sent dashboard heartbeat at: {time}", DateTimeOffset.Now);
        }
    }
}
