using Microsoft.Extensions.Options;
using RobotController.Infrastructure.Configuration;
using RobotController.Services.Constants;
using RobotController.Services.Interfaces;
using System.Text;

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
        _httpClient.Timeout = TimeSpan.FromSeconds(_robotApiOptions.ConnectionTimeoutInSeconds);
        _baseUrl = _robotApiOptions.BaseUrl ?? throw new ArgumentNullException(nameof(_robotApiOptions.BaseUrl));
    }

    public async Task MoveBackward(int speed)
    {
        using var response = await _httpClient.PostAsync($"{_baseUrl}{RobotApiEndpoints.Rear}", null);
        response.EnsureSuccessStatusCode();
    }

    public async Task MoveForward(int speed)
    {
        using var response = await _httpClient.PostAsync($"{_baseUrl}{RobotApiEndpoints.Front}", null);
        response.EnsureSuccessStatusCode();
    }

    public async Task MoveLeft()
    {
        using var response = await _httpClient.PostAsync($"{_baseUrl}{RobotApiEndpoints.Left}", null);
        response.EnsureSuccessStatusCode();
    }

    public async Task MoveRight()
    {
        using var response = await _httpClient.PostAsync($"{_baseUrl}{RobotApiEndpoints.Right}", null);
        response.EnsureSuccessStatusCode();
    }

    public async Task KeepAlive()
    {
        using var response = await _httpClient.GetAsync($"{_baseUrl}{RobotApiEndpoints.Keepalive}");
        response.EnsureSuccessStatusCode();
    }

    public async Task Talk(string? text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return;
        }

        var content = new StringContent(text, Encoding.UTF8, "text/plain");
        using var response = await _httpClient.PostAsync($"{_baseUrl}{RobotApiEndpoints.Talk}", content);
        response.EnsureSuccessStatusCode();
    }

    public async Task StartRecording()
    {
        using var response = await _httpClient.PostAsync($"{_baseUrl}{RobotApiEndpoints.StartRecording}", null);
        response.EnsureSuccessStatusCode();
    }

    public async Task EndRecording()
    {
        using var response = await _httpClient.PostAsync($"{_baseUrl}{RobotApiEndpoints.EndRecording}", null);
        response.EnsureSuccessStatusCode();
    }

    public async Task PlayLastRecordedRace()
    {
        using var response = await _httpClient.PostAsync($"{_baseUrl}{RobotApiEndpoints.PlayLastRecordedRace}", null);
        response.EnsureSuccessStatusCode();
    }
}
