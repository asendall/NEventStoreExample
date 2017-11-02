using System;

namespace NEventStoreExample.Infrastructure
{
    public class Event:IMessage
    {
        public Guid CorrelationId { get; protected set; }
    }
}