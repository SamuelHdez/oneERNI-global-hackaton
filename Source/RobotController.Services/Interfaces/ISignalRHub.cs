
namespace RobotController.Services.Interfaces
{
    public interface ISignalRHub
    {
        Task DisplayMessage(string message);

        Task ConnectionEvent(bool isRobotConnected);
    }
}
