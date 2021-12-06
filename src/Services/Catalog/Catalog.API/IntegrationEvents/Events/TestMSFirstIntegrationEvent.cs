using Microsoft.eShopOnDapr.BuildingBlocks.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.eShopOnDapr.Services.Catalog.API.IntegrationEvents.Events
{
    public class TestMSFirstIntegrationEvent : IntegrationEvent
    {
        public Guid Oid { get; set; }
        public string Name { get; set; }
        public TestMSFirstIntegrationEvent()
        {
            Oid = Guid.NewGuid();
            Name = DateTime.Now.ToString();
        }
    }
}
