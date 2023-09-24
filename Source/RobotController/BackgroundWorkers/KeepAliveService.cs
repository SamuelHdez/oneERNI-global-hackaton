using RobotController.Services;

namespace RobotController.BackgroundWorkers
{
    public class KeepAliveService : BackgroundService
    {
        private readonly ILogger<KeepAliveService> _logger;
        private readonly IRobotService robotService;

        public KeepAliveService(ILogger<KeepAliveService> logger, IRobotService robotService)
        {
            _logger = logger;
            this.robotService = robotService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await robotService.KeepAlive();
                }
                catch (Exception ex) 
                {
                    _logger.LogError(ex, "Error when trying to send the keep alive.");
                }

                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }
        }
    }
}
