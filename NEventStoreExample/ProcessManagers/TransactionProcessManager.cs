using CommonDomain.Persistence;
using MemBus;
using NEventStoreExample.Event;
using NEventStoreExample.Infrastructure;
using NEventStoreExample.Saga;

namespace NEventStoreExample.ProcessManagers
{
    public class TransactionProcessManager: ProcessManagerBase<TransactionSaga>,
        IEventHandler<TransactionCreatedEvent>,
        IEventHandler<AccountDebitedEvent>
    {

        public TransactionProcessManager(ISagaRepository repository, IBus bus) : base(repository, bus)
        {
        }


        public void Handle(TransactionCreatedEvent e)
        {
            Transition(e);
        }


        public void Handle(AccountDebitedEvent e)
        {
            Transition(e);
        }

 
    }
}
