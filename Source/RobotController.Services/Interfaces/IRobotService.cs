namespace RobotController.Services;

public interface IRobotService
{
    public Task Forward(int speed);

    public Task Backward(int speed);

    public Task SetDirection(int angle);
}
