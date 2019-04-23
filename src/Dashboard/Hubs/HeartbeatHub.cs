using Microsoft.AspNetCore.SignalR;

namespace Dashboard.Hubs
{
    public class HeartbeatHub : Hub
    {
        public void SendHeartbeat(string status, string robotName)
        {
            Clients.All.SendCoreAsync("heartbeatReceived", new object[] {
                status, robotName
            });
        }
    }
}