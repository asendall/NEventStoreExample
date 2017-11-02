using System;
using NEventStoreExample.Infrastructure;

namespace NEventStoreExample.Event
{
    [Serializable]
    public class AccountDebitedEvent: Infrastructure.Event
    {
        public AccountDebitedEvent(Guid correlationId, Guid accountId, decimal amount)
        {
            CorrelationId = correlationId;
            Id = accountId;
            Amount = amount;
        }

        public Guid Id { get; private set; }
        public decimal Amount { get; private set; }
    }
}
