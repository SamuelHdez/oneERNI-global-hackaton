using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
