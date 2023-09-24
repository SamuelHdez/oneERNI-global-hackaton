using Microsoft.Extensions.Options;
using Moq.Protected;
using RobotController.Infrastructure.Configuration;
using System.Net.Http.Json;
using System.Net;
using RobotController.Services.Constants;

namespace RobotController.Services.Tests;

public class RobotServiceTest
{
    private const string TestBaseUrl = "https://localhost:29378/api";
    private readonly Mock<HttpMessageHandler> httpMessageHandlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);

    [Fact]
    public void ShouldThrowError_WhenRobotApiOptionsIsNull()
    {
        // Arrange
        var httpClient = new HttpClient(httpMessageHandlerMock.Object);
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
        var robotApiOptions = new RobotApiOptions { BaseUrl = TestBaseUrl };
        var options = Options.Create(robotApiOptions);

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
        var httpClient = new HttpClient(httpMessageHandlerMock.Object);
        var robotApiOptions = new RobotApiOptions { BaseUrl = null };
        var options = Options.Create(robotApiOptions);

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
        var mockResponse = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK
        };

        httpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.Is<HttpRequestMessage>(m => RequestIsLeft(m)),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(mockResponse);
        var httpClient = new HttpClient(httpMessageHandlerMock.Object);
        var robotApiOptions = new RobotApiOptions { BaseUrl = TestBaseUrl };
        var options = Options.Create(robotApiOptions);
        var robotService = new RobotService(options, httpClient);

        // Act
        await robotService.MoveLeft();

        // Assert
        httpMessageHandlerMock
            .Protected()
            .Verify(
                "SendAsync",
                Times.Once(),
                ItExpr.Is<HttpRequestMessage>(m => RequestIsLeft(m)),
                ItExpr.IsAny<CancellationToken>()
        );
    }

    [Fact]
    public async Task ShouldSendMoveLeftCommandToRobotApi_WhenMoveRight()
    {
        // Arrange
        var mockResponse = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK
        };

        httpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.Is<HttpRequestMessage>(m => RequestIsRight(m)),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(mockResponse);
        var httpClient = new HttpClient(httpMessageHandlerMock.Object);
        var robotApiOptions = new RobotApiOptions { BaseUrl = TestBaseUrl };
        var options = Options.Create(robotApiOptions);
        var robotService = new RobotService(options, httpClient);

        // Act
        await robotService.MoveRight();

        // Assert
        httpMessageHandlerMock
            .Protected()
            .Verify(
                "SendAsync",
                Times.Once(),
                ItExpr.Is<HttpRequestMessage>(m => RequestIsRight(m)),
                ItExpr.IsAny<CancellationToken>()
        );
    }

    private static bool RequestIsLeft(HttpRequestMessage m) => m.Method == HttpMethod.Post && m.RequestUri!.AbsolutePath.EndsWith(RobotApiEndpoints.Left);

    private static bool RequestIsRight(HttpRequestMessage m) => m.Method == HttpMethod.Post && m.RequestUri!.AbsolutePath.EndsWith(RobotApiEndpoints.Right);
}