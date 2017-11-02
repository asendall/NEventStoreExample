using System;
using CommonDomain.Persistence;
using NEventStoreExample.Command;
using NEventStoreExample.Infrastructure;
using NEventStoreExample.Model;

namespace NEventStoreExample.CommandHandler
{
    public class CreateTransactionCommandHandler:ICommandHandler<CreateTransactionCommand>
    {
        private readonly IRepository _repository;

        public CreateTransactionCommandHandler(IRepository repository)
        {
            this._repository = repository;
        }
        public void Handle(CreateTransactionCommand command)
        {
            var transaction = new Transaction(new CorrelationId(command.CorrelationId),
                new TransactionId(command.Id),
                new AccountId(command.AccountToCreditId),
                new AccountId(command.AccountToDebitId),command.Amount);
            _repository.Save(transaction,Guid.NewGuid());
        }
    }
}
