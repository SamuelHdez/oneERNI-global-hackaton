namespace RobotController.Services.Interfaces;

public interface IRobotService
{
    Task MoveForward(int speed);

    Task MoveBackward(int speed);

    Task MoveLeft();

    Task MoveRight();

    Task KeepAlive();

    Task Talk(string? text);

    Task StartRecording();

    Task EndRecording();

    Task PlayLastRecordedRace();
}
