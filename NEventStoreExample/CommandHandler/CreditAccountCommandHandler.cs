using System;
using CommonDomain.Persistence;
using NEventStoreExample.Command;
using NEventStoreExample.Infrastructure;
using NEventStoreExample.Model;

namespace NEventStoreExample.CommandHandler
{
    public class CreditAccountCommandHandler : ICommandHandler<CreditAccountCommand>
    {
        private readonly IRepository _repository;

        public CreditAccountCommandHandler(IRepository repository)
        {
            this._repository = repository;
        }
        public void Handle(CreditAccountCommand command)
        {
            var account = _repository.GetById<Account>(command.AccountId);
            account.Credit(command.CorrelationId, command.Amount);
            _repository.Save(account, Guid.NewGuid());
        }


    }
}
