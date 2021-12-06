using Microsoft.eShopOnDapr.BuildingBlocks.EventBus.Abstractions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestMSApi.Infrastructure;
using TestMSApi.IntegrationEvents.Events;

namespace TestMSApi.IntegrationEvents.EventHandling
{
    public class TestMSFirstIntegrationEventHandler : IIntegrationEventHandler<TestMSFirstIntegrationEvent>
    {
        private readonly ILogger<TestMSFirstIntegrationEventHandler> _logger;
        public TestMSFirstIntegrationEventHandler(ILogger<TestMSFirstIntegrationEventHandler> logger)
        {
            _logger = logger;
        }
        public Task Handle(TestMSFirstIntegrationEvent @event) => Task.Run(() => _logger.LogInformation(@event.Name));
    }
}
