using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Messages.Events
{
    public class BaseIntegrationEvent
    {
        //This class will help us to track issues
        public Guid? CorrelationId { get; set; }
        public DateTime CreationDate { get; set; }
        public BaseIntegrationEvent()
        {
            CorrelationId = Guid.NewGuid();
            CreationDate = DateTime.Now;
        }

        public BaseIntegrationEvent(Guid correlationId, DateTime creationDate)
        {
            CorrelationId = correlationId;
            CreationDate = creationDate;
        }
    }
}
