using System;
using System.Collections.Generic;
using NEventStoreExample.Command;
using NEventStoreExample.CommandHandler;
using NEventStoreExample.Event;
using NEventStoreExample.Infrastructure;

namespace NEventStoreExample.Test
{
    public class when_creating_a_transaction : EventSpecification<CreateTransactionCommand>
    {
        private readonly Guid _correlationId = Guid.NewGuid();
        private readonly Guid _transactionId = Guid.NewGuid();
        private readonly Guid _accountToCreditId = Guid.NewGuid();
        private readonly Guid _accountToDebitId = Guid.NewGuid();
        private readonly decimal _amount = 100;
        protected override IEnumerable<Infrastructure.Event> Given()
        {
            yield break;
        }

        protected override CreateTransactionCommand When()
        {
            return new CreateTransactionCommand(_correlationId,_transactionId, _accountToCreditId, _accountToDebitId, _amount);
        }

        protected override ICommandHandler<CreateTransactionCommand> OnHandler()
        {
            return new CreateTransactionCommandHandler(Repository);
        }

        protected override IEnumerable<Infrastructure.Event> Expect()
        {
            yield return new TransactionCreatedEvent(_correlationId, _transactionId, _accountToCreditId, _accountToDebitId, _amount);
        }
    }
}
