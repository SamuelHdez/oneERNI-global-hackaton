using Microsoft.Extensions.Options;
using RobotController.Domain;
using RobotController.Infrastructure.Configuration;
using System.Net.Http.Json;

namespace RobotController.Services;

public class RobotService : IRobotService
{
    private readonly RobotApiOptions _robotApiOptions;
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;

    public RobotService(IOptions<RobotApiOptions> robotApiOptions, HttpClient httpClient)
    {
        _robotApiOptions = robotApiOptions?.Value ?? throw new ArgumentNullException(nameof(robotApiOptions)); ;
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _baseUrl = _robotApiOptions.BaseUrl ?? throw new ArgumentNullException(nameof(_robotApiOptions.BaseUrl));
    }

    public async Task MoveBackward(int speed)
    {
        // var speedModel = new SpeedDto { Speed = speed };
        // await _httpClient.PostAsJsonAsync($"{_baseUrl}/backward", speedModel);
        using var response = await _httpClient.PostAsync($"{_baseUrl}/rear", null);
        response.EnsureSuccessStatusCode();
    }

    public async Task MoveForward(int speed)
    {
        // var speedModel = new SpeedDto { Speed = speed };
        // await _httpClient.PostAsJsonAsync($"{_baseUrl}/forward", speedModel);
        using var response = await _httpClient.PostAsync($"{_baseUrl}/front", null);
        response.EnsureSuccessStatusCode();
    }

    public async Task SetDirection(int angle)
    {
        var angleModel = new AngleDto { Angle = angle };
        await _httpClient.PostAsJsonAsync($"{_baseUrl}/direction", angleModel);
    }

    public async Task MoveLeft()
    {
        using var response = await _httpClient.PostAsync($"{_baseUrl}/left", null);
        response.EnsureSuccessStatusCode();
    }

    public async Task MoveRight()
    {
        using var response = await _httpClient.PostAsync($"{_baseUrl}/right", null);
        response.EnsureSuccessStatusCode();
    }

    public async Task KeepAlive()
    {
        _httpClient.Timeout = TimeSpan.FromSeconds(1);
        using var response = await _httpClient.GetAsync($"{_baseUrl}/keepalive");
        response.EnsureSuccessStatusCode();
    }
}
