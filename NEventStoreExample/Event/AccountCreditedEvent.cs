using System;
using NEventStoreExample.Infrastructure;

namespace NEventStoreExample.Event
{
    [Serializable]
    public class AccountCreditedEvent: Infrastructure.Event
    {
        public AccountCreditedEvent(Guid correlationId, Guid accountId, decimal amount)
        {
            CorrelationId = correlationId;
            Id = accountId;
            Amount = amount;
        }

        public Guid Id { get; private set; }
        public decimal Amount { get; private set; }

    }
}
