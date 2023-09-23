
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
       // await _httpClient.PostAsJsonAsync($"{_baseUrl}/backward", speedModel);
        await _httpClient.GetAsync("https://jdarknessdomains.ddns.net:9800/rear");

    }

    public async Task Forward(int speed)
    {
        var speedModel = new SpeedDto { Speed = speed };
        //await _httpClient.PostAsJsonAsync($"{_baseUrl}/forward", speedModel);
        await _httpClient.GetAsync("https://jdarknessdomains.ddns.net:9800/front");
    }

    public async Task SetDirection(int angle)
    {
        var angleModel = new AngleDto { Angle = angle };
        await _httpClient.PostAsJsonAsync($"{_baseUrl}/direction", angleModel);
    }

    public async Task SetDirectionLeft(int angle)
    {
        await _httpClient.GetAsync("https://jdarknessdomains.ddns.net:9800/left");
    }

    public async Task SetDirectionRigth(int angle)
    {
        await _httpClient.GetAsync("https://jdarknessdomains.ddns.net:9800/right");
    }
}
