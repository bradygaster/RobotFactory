using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;

namespace Dashboard.Workers
{
    public class HeartbeatWorker : BackgroundService
    {
        private const string DASHBOARD_QUEUE_CONNECTION_STRING = "DASHBOARD_QUEUE_CONNECTION_STRING";
        private const string DASHBOARD_QUEUE = "dashboardheartbeat";

        public HeartbeatWorker(ILogger<HeartbeatWorker> logger)
        {
            Logger = logger;
        }

        public ILogger<HeartbeatWorker> Logger { get; }
        public CloudQueue CloudQueue { get; private set; }

        public override async Task StartAsync(CancellationToken stoppingToken)
        {
            Logger.LogInformation("Starting HeartbeatWorker at {time}", DateTimeOffset.Now);

            try
            {
                CloudStorageAccount storageAccount = null;
                
                Logger.LogInformation("HeartbeatWorker connecting to storage account at {time}", DateTimeOffset.Now);

                var success = CloudStorageAccount.TryParse(
                    Environment.GetEnvironmentVariable(DASHBOARD_QUEUE_CONNECTION_STRING),
                    out storageAccount
                );

                if(success)
                {
                    var queueClient = storageAccount.CreateCloudQueueClient();
                    CloudQueue = queueClient.GetQueueReference(DASHBOARD_QUEUE);
                    await CloudQueue.CreateIfNotExistsAsync();

                    Logger.LogInformation("HeartbeatWorker CONNECTED to storage account at {time}", DateTimeOffset.Now);
                }

                await base.StartAsync(stoppingToken);
            }
            catch(Exception ex)
            {
                Logger.LogError(ex, "Error connecting to storage");
            }
        }
        
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                Logger.LogInformation("Checking for dashboard heartbeats at {time}", DateTimeOffset.Now);
                var msg = await CloudQueue.GetMessageAsync();
                while (msg != null)
                {
                    Logger.LogInformation("Received heartbeat at {time}: {2} {1}",
                        DateTimeOffset.Now,
                        msg.AsString,
                        Environment.NewLine);

                    await CloudQueue.DeleteMessageAsync(msg);
                        
                    msg = await CloudQueue.GetMessageAsync();
                }
                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}