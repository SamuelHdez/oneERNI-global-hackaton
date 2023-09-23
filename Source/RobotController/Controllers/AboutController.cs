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
                    Description = "Hi, I’m Carmen! 🌟 With almost 5 years at ERNI, I’m known as the all-terrain woman.\nWhether it’s backend or frontend, I face everything with enthusiasm! 💻🚀 Sometimes I find myself caught between lines of code, and other times, bringing creative interfaces to life, always learning along the way.\n🌈✨ Ready or not, here I come, eager to tackle new challenges and learn even more in this hackathon! 🌟",
                    Avatar = "../../assets/images/carmen.png"
                },
                new TeamMember
                {
                    Name = "Ferran Balaguer",
                    Nickname = "All-terrain developer",
                    Description = "Hi, I’m Ferran! 🦖 With 5 years at ERNI, I’m something of a dinosaur – but not the extinct kind, the coding kind!\nI’ve been programming for as long as I can remember, maybe even before I learned to tie my shoes! 👟💻 I’ve seen programming languages come and go, and I’ve battled more bugs than a prehistoric creature. With every line of code, I bring a bit of ancient wisdom and a whole lot of experience\nLet’s make this hackathon go down in history! 🌋💾",
                    Avatar = "../../assets/images/ferran.png"
                },
                new TeamMember
                {
                    Name = "Samuel Hernández",
                    Nickname = "The flamingo",
                    Description = "Hello, I’m Samuel! 🚀 I’ve been at ERNI for 5 years, turning coffee into code and speaking a language that only computers understand.\n🧙‍♂️ Some say I was programmed in the basements of ERNI, but the truth is, I’ve been here so long even the bugs say good morning to me. I’m a proud member of Divertiteam, a squad of 5 people who turn 404 errors into 200 OK parties!\n🎉 Ready for the hackathon? Don’t be surprised if the keyboards start smoking! 🐞 Let’s go Divertiteam!",
                    Avatar = "../../assets/images/samuel.png"
                },
                new TeamMember
                {
                    Name = "David Soto",
                    Nickname = "Master Yoda",
                    Description = "Hello everyone, I’m David Soto! 🌐 With 5 years at ERNI, I’m the keeper of digital secrets and a cybersecurity geek.\nMy colleagues call me “Master Yoda” – but not because of my height, it’s because of my wisdom in keeping the cyber-Sith at bay! 🌌🚀 I have more tricks up my sleeve than a magician and I’m ready to teach the young Padawans the way of the Force… I mean, cybersecurity!\n💻🔒 May the Force of Security be with us in this hackathon! 🌟",
                    Avatar = "../../assets/images/david.png"

                },
                new TeamMember
                {
                    Name = "Andrés Vázquez",
                    Nickname = "The kid",
                    Description = "Hello everyone, I’m Andrés! 👋 I’m another full-stack wizard here, having been conjuring code at ERNI for around 2 and a half years.\nI look younger than my age, but must be all the screen glow giving me that youthful radiance! 💻✨ I’m the guy who’s always knee-deep in code, unraveling the mysteries of the programming universe. They say with great power comes great responsibility, but I just think it comes with more coffee!\n☕ Ready to roll up my sleeves and dive into the hackathon, let’s code up a storm! 🌪💻",
                    Avatar = "../../assets/images/andres.png"
                }
            };

            return members;
        }
    }
}