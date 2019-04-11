using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FactoryController
{
    public class Worker : BackgroundService
    {
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
                }
                catch(Exception ex)
                {
                    _logger.LogError(ex, "Error during dashboard test");
                }
            }
        }
    }
}
