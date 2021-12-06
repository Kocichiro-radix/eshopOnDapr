using Dapr;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestMSApi.IntegrationEvents.EventHandling;
using TestMSApi.IntegrationEvents.Events;

namespace TestMSApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class IntegrationEventController : ControllerBase
    {
        private const string DAPR_PUBSUB_NAME = "pubsub";

        [HttpPost("TestMSFirstIntegrationEvent")]
        [Topic(DAPR_PUBSUB_NAME, nameof(TestMSFirstIntegrationEvent))]
        public Task HandleAsync(
            TestMSFirstIntegrationEvent @event,
            [FromServices] TestMSFirstIntegrationEventHandler handler)
            => handler.Handle(@event);
    }
}
