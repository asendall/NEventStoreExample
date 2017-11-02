using System;
using NEventStoreExample.Infrastructure;

namespace NEventStoreExample.Event
{
    [Serializable]
    public class AccountClosedEvent : Infrastructure.Event
    {
        public AccountClosedEvent(Guid accountId)
        {
            Id = accountId;

        }

        public Guid Id { get; private set; }
    }
}