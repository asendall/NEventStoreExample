using System;
using System.Collections.Generic;
using NEventStoreExample.Command;
using NEventStoreExample.CommandHandler;
using NEventStoreExample.Event;
using NEventStoreExample.Infrastructure;
using NUnit.Framework;

namespace NEventStoreExample.Test
{
    
    [TestFixture]
    public class when_debiting_an_account : EventSpecification<DebitAccountCommand>
    {
        private readonly Guid _correlationId = Guid.NewGuid();
        private readonly Guid accountId = Guid.NewGuid();
        private decimal amount = 100;

        protected override IEnumerable<Infrastructure.Event> Given()
        {
            yield return new AccountCreatedEvent(accountId, "Luiz Damim", "@luizdamim", true);
        }

        protected override DebitAccountCommand When()
        {
            return new DebitAccountCommand(_correlationId, accountId, amount);
        }

        protected override ICommandHandler<DebitAccountCommand> OnHandler()
        {
            return new DebitAccountCommandHandler(Repository);
        }

        protected override IEnumerable<Infrastructure.Event> Expect()
        {
            yield return new AccountDebitedEvent(_correlationId,accountId, amount);
        }
    }
}
