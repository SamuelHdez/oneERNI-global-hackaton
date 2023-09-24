using Microsoft.Extensions.Options;
using Moq.Protected;
using RobotController.Infrastructure.Configuration;
using RobotController.Services.Constants;
using RobotController.Services.Tests.Extensions;
using System.Net;

namespace RobotController.Services.Tests;

public class RobotCameraServiceTest
{
    private const string TestBaseUrl = "https://localhost:29378/api";
    private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
    private delegate bool RequestCheckDelegate(HttpRequestMessage m);

    [Fact]
    public void ShouldThrowError_WhenRobotApiOptionsIsNull()
    {
        // Arrange
        var httpClient = new HttpClient(_httpMessageHandlerMock.Object);
        var nullOptions = Options.Create<RobotApiOptions>(null!);

        // Act
        Action act = () => new RobotCameraService(nullOptions, httpClient);

        // Assert
        act.Should().Throw<ArgumentNullException>()
            .WithMessage($"*{nameof(RobotApiOptions)}*");
    }

    [Fact]
    public void ShouldThrowError_WhenHttpClientIsNull()
    {
        // Arrange
        var options = SetupRobotApiOptions(TestBaseUrl);

        // Act
        Action act = () => new RobotCameraService(options, null!);

        // Assert
        act.Should().Throw<ArgumentNullException>()
            .WithMessage($"*{nameof(HttpClient)}*");
    }

    [Fact]
    public void ShouldThrowError_WhenRobotApiBaseUrlIsNull()
    {
        // Arrange
        var httpClient = new HttpClient(_httpMessageHandlerMock.Object);
        var options = SetupRobotApiOptions(null);

        // Act
        Action act = () => new RobotCameraService(options, httpClient);

        // Assert
        act.Should().Throw<ArgumentNullException>()
            .WithMessage($"*{nameof(RobotApiOptions.BaseUrl)}*");
    }

    [Fact]
    public async Task ShouldSendCameraUpCommandToRobotApi_WhenCameraUp()
    {
        // Arrange
        var httpClient = SetupHttpClient(RequestIsCameraUp);
        var options = SetupRobotApiOptions(TestBaseUrl);
        var robotCameraService = new RobotCameraService(options, httpClient);

        // Act
        await robotCameraService.CameraUp();

        // Assert
        VerifyRequest(RequestIsCameraUp);
    }

    [Fact]
    public async Task ShouldSendCameraDownCommandToRobotApi_WhenCameraDown()
    {
        // Arrange
        var httpClient = SetupHttpClient(RequestIsCameraDown);
        var options = SetupRobotApiOptions(TestBaseUrl);
        var robotCameraService = new RobotCameraService(options, httpClient);

        // Act
        await robotCameraService.CameraDown();

        // Assert
        VerifyRequest(RequestIsCameraDown);
    }

    [Fact]
    public async Task ShouldSendCameraLeftCommandToRobotApi_WhenCameraLeft()
    {
        // Arrange
        var httpClient = SetupHttpClient(RequestIsCameraLeft);
        var options = SetupRobotApiOptions(TestBaseUrl);
        var robotCameraService = new RobotCameraService(options, httpClient);

        // Act
        await robotCameraService.CameraLeft();

        // Assert
        VerifyRequest(RequestIsCameraLeft);
    }

    [Fact]
    public async Task ShouldSendCameraRightCommandToRobotApi_WhenCameraRight()
    {
        // Arrange
        var httpClient = SetupHttpClient(RequestIsCameraRight);
        var options = SetupRobotApiOptions(TestBaseUrl);
        var robotCameraService = new RobotCameraService(options, httpClient);

        // Act
        await robotCameraService.CameraRight();

        // Assert
        VerifyRequest(RequestIsCameraRight);
    }

    [Fact]
    public async Task ShouldSendCameraCenterCommandToRobotApi_WhenCameraCenter()
    {
        // Arrange
        var httpClient = SetupHttpClient(RequestIsCameraCenter);
        var options = SetupRobotApiOptions(TestBaseUrl);
        var robotCameraService = new RobotCameraService(options, httpClient);

        // Act
        await robotCameraService.CameraCenter();

        // Assert
        VerifyRequest(RequestIsCameraCenter);
    }

    private HttpClient SetupHttpClient(RequestCheckDelegate requestCheck)
    {
        var mockResponse = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK
        };

        _httpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.Is<HttpRequestMessage>(m => requestCheck(m)),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(mockResponse);
        var httpClient = new HttpClient(_httpMessageHandlerMock.Object);
        return httpClient;
    }

    private void VerifyRequest(RequestCheckDelegate requestCheck)
    {
        _httpMessageHandlerMock
           .Protected()
           .Verify(
               "SendAsync",
               Times.Once(),
               ItExpr.Is<HttpRequestMessage>(m => requestCheck(m)),
               ItExpr.IsAny<CancellationToken>()
       );
    }

    private IOptions<RobotApiOptions> SetupRobotApiOptions(string? baseUrl)
    {
        var robotApiOptions = new RobotApiOptions { BaseUrl = baseUrl };
        return Options.Create(robotApiOptions);
    }

    private static bool RequestIsCameraUp(HttpRequestMessage m) => m.RequestIsPostAndEndsWith(RobotApiEndpoints.CameraUp);

    private static bool RequestIsCameraDown(HttpRequestMessage m) => m.RequestIsPostAndEndsWith(RobotApiEndpoints.CameraDown);

    private static bool RequestIsCameraLeft(HttpRequestMessage m) => m.RequestIsPostAndEndsWith(RobotApiEndpoints.CameraLeft);

    private static bool RequestIsCameraRight(HttpRequestMessage m) => m.RequestIsPostAndEndsWith(RobotApiEndpoints.CameraRight);

    private static bool RequestIsCameraCenter(HttpRequestMessage m) => m.RequestIsPostAndEndsWith(RobotApiEndpoints.CameraCenter);
}
