namespace RobotController.Services.Interfaces
{
    public interface IRobotCameraService
    {
        Task CameraDown();
        Task CameraLeft();
        Task CameraRight();
        Task CameraUp();
        Task CameraCenter();
    }
}