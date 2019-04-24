using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Dashboard.Hubs
{
    public class HeartbeatHub : Hub
    {
        public async Task SendHeartbeat(string status, 
            string robotName,
            int batteryLevel)
        {
            await Clients.All.SendCoreAsync("heartbeatReceived", 
                new object[] 
                {
                    status,
                    robotName,
                    batteryLevel
                });
        }
    }
}