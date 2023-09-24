using RobotController.Domain.Dtos;

namespace RobotController.Domain.Models
{
    public class ConnectionEvent
    {
        public bool IsConnected { get; set; }

        public DateTime DateTime { get; set; }

        public ConnectionEvent() { }

        public ConnectionEvent(ConnectionEventDto connectionEventDto)
        {
            this.IsConnected = connectionEventDto.IsConnected;
            this.DateTime = DateTime.UtcNow;
        }
    }
}
