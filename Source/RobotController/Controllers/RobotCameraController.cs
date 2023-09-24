using Microsoft.AspNetCore.Mvc;
using RobotController.Services.Interfaces;

namespace RobotController.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RobotCameraController : ControllerBase
    {
        private readonly ILogger<RobotCameraController> _logger;
        private readonly IRobotCameraService _robotCameraService;

        public RobotCameraController(ILogger<RobotCameraController> logger, IRobotCameraService robotCameraService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _robotCameraService = robotCameraService ?? throw new ArgumentNullException(nameof(robotCameraService));
        }

        [HttpPost("up")]
        public async Task<IActionResult> CameraUp()
        {
            await _robotCameraService.CameraUp();
            return Ok();
        }

        [HttpPost("down")]
        public async Task<IActionResult> CameraDown()
        {
            await _robotCameraService.CameraDown();
            return Ok();
        }

        [HttpPost("left")]
        public async Task<IActionResult> CameraLeft()
        {
            await _robotCameraService.CameraLeft();
            return Ok();
        }

        [HttpPost("right")]
        public async Task<IActionResult> CameraRight()
        {
            await _robotCameraService.CameraRight();
            return Ok();
        }
    }
}
