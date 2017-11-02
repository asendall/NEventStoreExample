using System;
using NEventStoreExample.Infrastructure;

namespace NEventStoreExample.Command
{
    public class CreateTransactionCommand:Infrastructure.Command
    {
        public CreateTransactionCommand(Guid correlationId, Guid transactionId, Guid accountToCreditId, Guid accountToDebitId, decimal amount)
        {
            CorrelationId = correlationId;
            Id = transactionId;
            AccountToCreditId = accountToCreditId;
            AccountToDebitId = accountToDebitId;
            Amount = amount;
        }

        public Guid CorrelationId { get; private set; }
        public Guid Id { get; private set; }
        public Guid AccountToCreditId { get; private set; }
        public Guid AccountToDebitId { get; private set; }
        public decimal Amount { get; private set; }
    }
}
