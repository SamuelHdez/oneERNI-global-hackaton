using Microsoft.AspNetCore.SignalR;
using RobotController.Domain.Dtos;
using RobotController.Domain.Models;
using RobotController.Services.Hubs;
using RobotController.Services.Interfaces;

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
                    await SendConnectionEvent(true);

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error when trying to send the keep alive.");
                    await SendConnectionEvent(false);
                }

                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
            }
        }

        private async Task SendConnectionEvent(bool isConnected)
        {
            var connectionEvent = new ConnectionEventDto
            {
                IsConnected = isConnected
            };

            await _hub.Clients.All.SendAsync(nameof(ConnectionEvent), connectionEvent);
        }
    }
}
