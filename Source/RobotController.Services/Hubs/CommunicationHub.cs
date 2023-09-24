using Microsoft.AspNetCore.SignalR;
using RobotController.Services.Interfaces;

namespace RobotController.Services.Hubs
{
    public class CommunicationHub : Hub<ISignalRHub>
    {
        public void Hello()
        {
            Clients.Caller.DisplayMessage("Hello from the SignalrDemoHub!");
        }

        public void RaiseConnectionEvent(bool isRobotConnected)
        {
            Clients.Caller.ConnectionEvent(isRobotConnected);
        }
    }
}
