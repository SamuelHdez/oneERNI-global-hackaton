using Microsoft.Extensions.Options;
using RobotController.Infrastructure.Configuration;
using RobotController.Services.Constants;
using RobotController.Services.Interfaces;

namespace RobotController.Services;

public class RobotCameraService : IRobotCameraService
{
    private readonly RobotApiOptions _robotApiOptions;
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;

    public RobotCameraService(IOptions<RobotApiOptions> robotApiOptions, HttpClient httpClient)
    {
        _robotApiOptions = robotApiOptions?.Value ?? throw new ArgumentNullException(nameof(robotApiOptions)); ;
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _httpClient.Timeout = TimeSpan.FromSeconds(_robotApiOptions.ConnectionTimeoutInSeconds);
        _baseUrl = _robotApiOptions.BaseUrl ?? throw new ArgumentNullException(nameof(_robotApiOptions.BaseUrl));
    }

    public async Task CameraUp()
    {
        using var response = await _httpClient.PostAsync($"{_baseUrl}{RobotApiEndpoints.CameraUp}", null);
        response.EnsureSuccessStatusCode();
    }

    public async Task CameraDown()
    {
        using var response = await _httpClient.PostAsync($"{_baseUrl}{RobotApiEndpoints.CameraDown}", null);
        response.EnsureSuccessStatusCode();
    }

    public async Task CameraLeft()
    {
        using var response = await _httpClient.PostAsync($"{_baseUrl}{RobotApiEndpoints.CameraLeft}", null);
        response.EnsureSuccessStatusCode();
    }

    public async Task CameraRight()
    {
        using var response = await _httpClient.PostAsync($"{_baseUrl}{RobotApiEndpoints.CameraRight}", null);
        response.EnsureSuccessStatusCode();
    }

    public async Task CameraCenter()
    {
        using var response = await _httpClient.PostAsync($"{_baseUrl}{RobotApiEndpoints.CameraCenter}", null);
        response.EnsureSuccessStatusCode();
    }
}
