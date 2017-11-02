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
    public class when_crediting_an_account : EventSpecification<CreditAccountCommand>
    {
        private readonly Guid _correlationId = Guid.NewGuid();
        private readonly Guid accountId = Guid.NewGuid();
        private decimal amount = 100;

        protected override IEnumerable<Infrastructure.Event> Given()
        {
            yield return new AccountCreatedEvent(accountId, "Luiz Damim", "@luizdamim", true);
        }

        protected override CreditAccountCommand When()
        {
            return new CreditAccountCommand(_correlationId, accountId, amount);
        }

        protected override ICommandHandler<CreditAccountCommand> OnHandler()
        {
            return new CreditAccountCommandHandler(Repository);
        }

        protected override IEnumerable<Infrastructure.Event> Expect()
        {
            yield return new AccountCreditedEvent(_correlationId, accountId, amount);
        }
    }
}
