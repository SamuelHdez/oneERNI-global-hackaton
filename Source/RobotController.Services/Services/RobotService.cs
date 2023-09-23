
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.Extensions.Options;
using RobotController.Domain;
using RobotController.Infrastructure.Configuration;

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

    public async Task Backward(int speed)
    {
        var speedModel = new SpeedDto { Speed = speed };
        await _httpClient.PostAsJsonAsync($"{_baseUrl}/backward", speedModel);
    }

    public async Task Forward(int speed)
    {
        var speedModel = new SpeedDto { Speed = speed };
        await _httpClient.PostAsJsonAsync($"{_baseUrl}/forward", speedModel);
    }

    public async Task SetDirection(int angle)
    {
        var angleModel = new AngleDto { Angle = angle };
        await _httpClient.PostAsJsonAsync($"{_baseUrl}/direction", angleModel);
    }
}
