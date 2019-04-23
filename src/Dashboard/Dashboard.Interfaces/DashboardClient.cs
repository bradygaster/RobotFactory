using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace Dashboard.Interfaces
{
    public class DashboardClient
    {
        private string RobotName { get; set; }
        private HubConnection Connection { get; set; }

        public static DashboardClient Start(string robotName)
        {
            var ret = new DashboardClient();
            ret.Connection = new HubConnectionBuilder()
                .WithUrl("http://dashboard/heartbeat")
                .Build();

            ret.Connection.StartAsync().Wait();

            return ret;
        }

        public async Task SendStatus(string status)
        {
            await Connection.InvokeAsync("heartbeatReceived", 
                status,
                RobotName);
        }
    } 
}