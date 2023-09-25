namespace RobotController.Services.Tests.Extensions;

public static class HttpRequestMessageExtension
{
    public static bool RequestIsPostAndEndsWith(this HttpRequestMessage requestMessage, string endsWith) => requestMessage.Method == HttpMethod.Post && requestMessage.RequestUri!.AbsolutePath.EndsWith(endsWith);
}
