using System;
using CommonDomain.Core;
using NEventStoreExample.Event;

namespace NEventStoreExample.Model
{
    public class Transaction : AggregateBase
    {
        public Transaction(CorrelationId correlationId, TransactionId id, AccountId accountToCreditId, AccountId accountToDebitId, decimal amount): this(id.Value)
        {
            RaiseEvent(new TransactionCreatedEvent(correlationId.Value, Id.Value, accountToCreditId.Value, accountToDebitId.Value, amount));
        }

        private Transaction(Guid id)
        {
            Id = new TransactionId(id);
        }

        public CorrelationId CorrelationId { get; private set; }
        public new TransactionId Id { get; private set; }
        public AccountId AccountToCreditId { get; private set; }
        public AccountId AccountToDebitId { get; private set; }
        public decimal Amount { get; private set; }

        private void Apply(TransactionCreatedEvent @event)
        {
            CorrelationId = new CorrelationId(@event.CorrelationId);
            Id = new TransactionId(@event.Id);
            AccountToCreditId = new AccountId(@event.AccountToCreditId);
            AccountToDebitId = new AccountId(@event.AccountToDebitId);
            Amount = @event.Amount;
        }
    }
}
