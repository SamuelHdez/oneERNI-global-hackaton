using Microsoft.AspNetCore.SignalR;
using RobotController.Domain.Dtos;
using RobotController.Domain.Models;
using RobotController.Services;
using RobotController.Services.Hubs;

namespace RobotController.BackgroundWorkers
{
    public class KeepAliveService : BackgroundService
    {
        private readonly ILogger<KeepAliveService> _logger;
        private readonly IRobotService _robotService;
        private readonly IHubContext<CommunicationHub> _hub;

        public KeepAliveService(ILogger<KeepAliveService> logger, IRobotService robotService, IHubContext<CommunicationHub> hub)
        {
            _logger = logger;
            _robotService = robotService;
            _hub = hub;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await _robotService.KeepAlive();
                    var connectionEvent = new ConnectionEvent
                    {
                        IsConnected = true,
                        DateTime = DateTime.UtcNow,
                    };
                    await _hub.Clients.All.SendAsync(nameof(ConnectionEvent), connectionEvent);

                }
                catch (Exception ex) 
                {
                    _logger.LogError(ex, "Error when trying to send the keep alive.");

                    var connectionEvent = new ConnectionEvent
                    {
                        IsConnected = false,
                        DateTime = DateTime.UtcNow,
                    };

                    await _hub.Clients.All.SendAsync(nameof(ConnectionEvent), connectionEvent);
                }

                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }
        }
    }
}
