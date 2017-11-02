using System;
using CommonDomain;
using CommonDomain.Persistence;
using MemBus;

namespace NEventStoreExample.Infrastructure
{
    public class ProcessManagerBase<TSaga> where TSaga : class, ISaga
    {
        private readonly ISagaRepository _repository;
        private readonly IBus _bus;
        protected ProcessManagerBase(ISagaRepository repository, IBus bus)
        {
            this._repository = repository;
            _bus = bus;
        }
        protected void Transition(Infrastructure.Event e)
        {

            ISaga saga = _repository.GetById<TSaga>(e.CorrelationId);
            saga.Transition(e);
            var undispatchedMessages = saga.GetUndispatchedMessages();
            foreach (var c in undispatchedMessages)
            {
                _bus.Publish(c);
            }
            _repository.Save(saga, Guid.NewGuid(), null);

        }
    }
}
