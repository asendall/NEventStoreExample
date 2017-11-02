using System;
using CommonDomain.Persistence;
using NEventStoreExample.Command;
using NEventStoreExample.Infrastructure;
using NEventStoreExample.Model;

namespace NEventStoreExample.CommandHandler
{
    public class DebitAccountCommandHandler : ICommandHandler<DebitAccountCommand>
    {
        private readonly IRepository _repository;

        public DebitAccountCommandHandler(IRepository repository)
        {
            this._repository = repository;
        }
        public void Handle(DebitAccountCommand command)
        {
            var account = _repository.GetById<Account>(command.AccountId);
            account.Debit(command.CorrelationId, command.Amount);
            _repository.Save(account, Guid.NewGuid());
        }
    }
}
