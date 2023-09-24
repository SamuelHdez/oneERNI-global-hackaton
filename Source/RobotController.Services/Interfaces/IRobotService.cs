namespace RobotController.Services;

public interface IRobotService
{
    public Task MoveForward(int speed);

    public Task MoveBackward(int speed);

    public Task SetDirection(int angle);

    public Task MoveLeft();

    public Task MoveRight();
}
