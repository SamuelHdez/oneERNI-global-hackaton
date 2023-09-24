using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotController.Services.Constants;

public static class RobotApiEndpoints
{
    public const string Rear = "/rear";
    public const string Front = "/front";
    public const string Talk = "/talk";
    public const string Left = "/left";
    public const string Right = "/right";
    public const string Keepalive = "/keepalive";
    public const string CameraLeft = "/cleft";
    public const string CameraRight = "/cright";
    public const string CameraUp = "/cup";
    public const string CameraDown = "/cdown";
    public const string StartRecording = "/startrecording";
    public const string EndRecording = "/endrecording";
    public const string PlayLastRecordedRace = "/playlastrecordedrace";
}
