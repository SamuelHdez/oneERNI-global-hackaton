using Microsoft.AspNetCore.Mvc;
using RobotController.Domain.Models;

namespace RobotController.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AboutController : ControllerBase
    {
        private readonly ILogger<AboutController> _logger;

        public AboutController(ILogger<AboutController> logger)
        {
            _logger = logger;
        }

        [HttpGet("TeamMembers")]
        public IEnumerable<TeamMember> Get()
        {
            var members = new List<TeamMember>
            {
                new TeamMember
                {
                    Name = "Carmen Avram",
                    Nickname = "All-terrain developer",
                    Description = "This is the description for Carmen Avram",
                    Avatar = "../../assets/images/carmen.png"
                },
                new TeamMember
                {
                    Name = "Ferran Balaguer",
                    Nickname = "All-terrain developer",
                    Description = "This is the description for Ferran Balaguer",
                    Avatar = "../../assets/images/samuel.png"
                },
                new TeamMember
                {
                    Name = "Samuel Hernández",
                    Nickname = "The flamingo",
                    Description = "This is the description for Samuel Hernández",
                    Avatar = "../../assets/images/samuel.png"
                },
                new TeamMember
                {
                    Name = "David Soto",
                    Nickname = "Master Yoda",
                    Description = "This is the description for David Soto",
                    Avatar = "../../assets/images/david.png"

                },
                new TeamMember
                {
                    Name = "Andrés Vázquez",
                    Nickname = "The kid",
                    Description = "This is the description for Andrés Vázquez",
                    Avatar = "../../assets/images/samuel.png"
                }
            };

            return members;
        }
    }
}