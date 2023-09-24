using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using RobotController.Domain;
using RobotController.Domain.Dtos;
using RobotController.Domain.Models;
using RobotController.Services.Hubs;
using RobotController.Services.Interfaces;

namespace RobotController.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RobotCommandController : ControllerBase
    {
        private readonly IHubContext<CommunicationHub> _hub;
        private readonly ILogger<RobotCommandController> _logger;
        private readonly IRobotService _robotService;

        public RobotCommandController(ILogger<RobotCommandController> logger, IHubContext<CommunicationHub> hub, IRobotService robotService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _hub = hub ?? throw new ArgumentNullException(nameof(hub));
            _robotService = robotService ?? throw new ArgumentNullException(nameof(robotService));
        }

        [HttpPost("connection")]
        public async Task<IActionResult> Connection([FromBody] ConnectionEventDto connectionEventDto)
        {
            await _hub.Clients.All.SendAsync(nameof(ConnectionEvent), new ConnectionEvent(connectionEventDto));
            return Ok();
        }

        [HttpPost("forward")]
        public async Task<IActionResult> MoveForward([FromBody] SpeedDto speedDto)
        {
            await _robotService.MoveForward(speedDto.Speed);
            return Ok();
        }

        [HttpPost("backward")]
        public async Task<IActionResult> MoveBackward([FromBody] SpeedDto speedDto)
        {
            await _robotService.MoveBackward(speedDto.Speed);
            return Ok();
        }

        [HttpPost("left")]
        public async Task<IActionResult> MoveLeft([FromBody] AngleDto angleDto)
        {
            await _robotService.MoveLeft();
            return Ok();
        }

        [HttpPost("right")]
        public async Task<IActionResult> MoveRight([FromBody] AngleDto angleDto)
        {
            await _robotService.MoveRight();
            return Ok();
        }

        [HttpPost("talk")]
        public async Task<IActionResult> Talk([FromBody] TalkDto talkDto)
        {
            await _robotService.Talk(talkDto.Text);
            return Ok();
        }

        [HttpPost("start-recording")]
        public async Task<IActionResult> StartRecording()
        {
            await _robotService.StartRecording();
            return Ok();
        }

        [HttpPost("end-recording")]
        public async Task<IActionResult> EndRecording()
        {
            await _robotService.EndRecording();
            return Ok();
        }

        [HttpPost("play-last-recorded")]
        public async Task<IActionResult> PlayLastRecordedRace()
        {
            await _robotService.PlayLastRecordedRace();
            return Ok();
        }
    }
}