using Microsoft.Extensions.Options;
using Moq.Protected;
using RobotController.Infrastructure.Configuration;
using RobotController.Services.Constants;
using RobotController.Services.Tests.Extensions;
using System.Net;

namespace RobotController.Services.Tests;

public class RobotServiceTest
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
        Action act = () => new RobotService(nullOptions, httpClient);

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
        Action act = () => new RobotService(options, null!);

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
        Action act = () => new RobotService(options, httpClient);

        // Assert
        act.Should().Throw<ArgumentNullException>()
            .WithMessage($"*{nameof(RobotApiOptions.BaseUrl)}*");
    }

    [Fact]
    public async Task ShouldSendMoveLeftCommandToRobotApi_WhenMoveLeft()
    {
        // Arrange
        var httpClient = SetupHttpClient(RequestIsLeft);
        var options = SetupRobotApiOptions(TestBaseUrl);
        var robotService = new RobotService(options, httpClient);

        // Act
        await robotService.MoveLeft();

        // Assert
        VerifyRequest(RequestIsLeft);
    }

    [Fact]
    public async Task ShouldSendMoveRightCommandToRobotApi_WhenMoveRight()
    {
        // Arrange
        var httpClient = SetupHttpClient(RequestIsRight);
        var options = SetupRobotApiOptions(TestBaseUrl);
        var robotService = new RobotService(options, httpClient);

        // Act
        await robotService.MoveRight();

        // Assert
        VerifyRequest(RequestIsRight);
    }

    [Fact]
    public async Task ShouldSendMoveForwardCommandToRobotApi_WhenMoveForward()
    {
        // Arrange
        var httpClient = SetupHttpClient(RequestIsForward);
        var options = SetupRobotApiOptions(TestBaseUrl);
        var robotService = new RobotService(options, httpClient);

        // Act
        await robotService.MoveForward(10);

        // Assert
        VerifyRequest(RequestIsForward);
    }

    [Fact]
    public async Task ShouldSendMoveBackwardCommandToRobotApi_WhenMoveBackward()
    {
        // Arrange
        var httpClient = SetupHttpClient(RequestIsBackward);
        var options = SetupRobotApiOptions(TestBaseUrl);
        var robotService = new RobotService(options, httpClient);

        // Act
        await robotService.MoveBackward(10);

        // Assert
        VerifyRequest(RequestIsBackward);
    }

    [Fact]
    public async Task ShouldSendTalkCommandToRobotApi_WhenTalk()
    {
        // Arrange
        var text = "Hello, I am Francesco Virgolini!";
        var mockResponse = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK
        };

        _httpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.Is<HttpRequestMessage>(m => RequestIsTalk(m, text)),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(mockResponse);
        var httpClient = new HttpClient(_httpMessageHandlerMock.Object);
        var robotApiOptions = new RobotApiOptions { BaseUrl = TestBaseUrl };
        var options = Options.Create(robotApiOptions);
        var robotService = new RobotService(options, httpClient);

        // Act
        await robotService.Talk(text);

        // Assert
        _httpMessageHandlerMock
            .Protected()
            .Verify(
                "SendAsync",
                Times.Once(),
                ItExpr.Is<HttpRequestMessage>(m => RequestIsTalk(m, text)),
                ItExpr.IsAny<CancellationToken>()
        );
    }

    [Fact]
    public async Task ShouldSendStartRecordingCommandToRobotApi_WhenStartRecording()
    {
        // Arrange
        var httpClient = SetupHttpClient(RequestIsStartRecording);
        var options = SetupRobotApiOptions(TestBaseUrl);
        var robotService = new RobotService(options, httpClient);

        // Act
        await robotService.StartRecording();

        // Assert
        VerifyRequest(RequestIsStartRecording);
    }

    [Fact]
    public async Task ShouldSendEndRecordingCommandToRobotApi_WhenEndRecording()
    {
        // Arrange
        var httpClient = SetupHttpClient(RequestIsEndRecording);
        var options = SetupRobotApiOptions(TestBaseUrl);
        var robotService = new RobotService(options, httpClient);

        // Act
        await robotService.EndRecording();

        // Assert
        VerifyRequest(RequestIsEndRecording);
    }

    [Fact]
    public async Task ShouldSendPlayLastRecordedRaceCommandToRobotApi_WhenPlayLastRecordedRace()
    {
        // Arrange
        var httpClient = SetupHttpClient(RequestIsPlayLastRecordedRace);
        var options = SetupRobotApiOptions(TestBaseUrl);
        var robotService = new RobotService(options, httpClient);

        // Act
        await robotService.PlayLastRecordedRace();

        // Assert
        VerifyRequest(RequestIsPlayLastRecordedRace);
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

    private static bool RequestIsLeft(HttpRequestMessage m) => m.RequestIsPostAndEndsWith(RobotApiEndpoints.Left);

    private static bool RequestIsRight(HttpRequestMessage m) => m.RequestIsPostAndEndsWith(RobotApiEndpoints.Right);

    private static bool RequestIsForward(HttpRequestMessage m) => m.RequestIsPostAndEndsWith(RobotApiEndpoints.Front);

    private static bool RequestIsBackward(HttpRequestMessage m) => m.RequestIsPostAndEndsWith(RobotApiEndpoints.Rear);

    private static bool RequestIsTalk(HttpRequestMessage m, string text) => m.RequestIsPostAndEndsWith(RobotApiEndpoints.Talk) && m.Content?.ReadAsStringAsync().Result! == text;

    private static bool RequestIsStartRecording(HttpRequestMessage m) => m.RequestIsPostAndEndsWith(RobotApiEndpoints.StartRecording);

    private static bool RequestIsEndRecording(HttpRequestMessage m) => m.RequestIsPostAndEndsWith(RobotApiEndpoints.EndRecording);

    private static bool RequestIsPlayLastRecordedRace(HttpRequestMessage m) => m.RequestIsPostAndEndsWith(RobotApiEndpoints.PlayLastRecordedRace);
}