using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using RobotController.Domain.Dtos;
using RobotController.Domain.Models;
using RobotController.Services.Hubs;

namespace RobotController.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RobotController : ControllerBase
    {

        private readonly IHubContext<CommunicationHub> _hub;
        private readonly ILogger<RobotController> _logger;

        public RobotController(ILogger<RobotController> logger, IHubContext<CommunicationHub> hub)
        {
            _logger = logger;
            _hub = hub;
        }

        [HttpPost("Connection")]
        public void ConnectionEvent([FromBody] ConnectionEventDto connectionEventDto)
        {
            _hub.Clients.All.SendAsync("ConnectionEvent", new ConnectionEvent(connectionEventDto));
        }
    }
}