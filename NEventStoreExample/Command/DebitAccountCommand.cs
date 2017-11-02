using System;
using NEventStoreExample.Infrastructure;

namespace NEventStoreExample.Command
{
    public class DebitAccountCommand:Infrastructure.Command
    {
        public DebitAccountCommand(Guid correlationId, Guid accountId, decimal amount)
        {
            CorrelationId = correlationId;
            AccountId = accountId;
            Amount = amount;
        }

        public Guid CorrelationId { get; private set; }
        public Guid AccountId { get; private set; }
        public decimal Amount { get; private set; }
    }
}
