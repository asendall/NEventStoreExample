using System;
using NEventStoreExample.Infrastructure;

namespace NEventStoreExample.Command
{
    public class CreditAccountCommand:Infrastructure.Command
    {
        public CreditAccountCommand(Guid correlationId, Guid accountId, decimal amount )
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
