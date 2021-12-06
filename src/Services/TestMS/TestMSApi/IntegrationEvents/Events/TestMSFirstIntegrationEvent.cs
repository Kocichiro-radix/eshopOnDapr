using Microsoft.eShopOnDapr.BuildingBlocks.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestMSApi.IntegrationEvents.Events
{
    public class TestMSFirstIntegrationEvent : IntegrationEvent
    {
        public Guid Oid { get; set; }
        public string Name { get; set; }

        public TestMSFirstIntegrationEvent() 
        { 
        }

        public TestMSFirstIntegrationEvent(Guid oid,string name)
        {
            Oid = oid;
            Name = name;
        }
    }
}
