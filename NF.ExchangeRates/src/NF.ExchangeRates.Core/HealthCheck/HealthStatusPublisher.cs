using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace NF.ExchangeRates.Core.HealthCheck
{
    public class HealthStatusPublisher : IHealthCheckPublisher
    {
        private readonly HealthStatusTracker _statusTracker = new HealthStatusTracker() { Previous = HealthStatus.Unhealthy };

        private readonly ILogger<HealthStatusPublisher> _logger;
        private readonly IOptions<HealthCheckPublisherConfiguration> _configuration;
        public HealthStatusPublisher(ILogger<HealthStatusPublisher> logger, IOptions<HealthCheckPublisherConfiguration> configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public Task PublishAsync(HealthReport report, CancellationToken cancellationToken)
        {
            if (_statusTracker.Current != report.Status)
            {
                _statusTracker.Current = report.Status;
                _statusTracker.Count = 1;
            }
            else
            {
                _statusTracker.Count = _statusTracker.Count >= int.MaxValue ? _statusTracker.Count : _statusTracker.Count + 1;
            }

            if (_statusTracker.Count >= _configuration.Value.StatusChangeTolerance && _statusTracker.Previous != _statusTracker.Current)
            {
                _logger.LogInformation("Health status changed from {PreviousStatus} to {CurrentStatus}", _statusTracker.Previous, _statusTracker.Current);
                _statusTracker.Previous = _statusTracker.Current;
            }


            cancellationToken.ThrowIfCancellationRequested();
            return Task.CompletedTask;
        }
    }
}
