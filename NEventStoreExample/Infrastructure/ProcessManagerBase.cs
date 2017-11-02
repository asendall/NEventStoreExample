using System;
using CommonDomain;
using CommonDomain.Persistence;

namespace NEventStoreExample.Infrastructure
{
    public class ProcessManagerBase<TSaga> where TSaga : class, ISaga
    {
        private readonly ISagaRepository _repository;
        protected ProcessManagerBase(ISagaRepository repository)
        {
            this._repository = repository;
        }
        protected void Transition(Event e)
        {

            ISaga saga = _repository.GetById<TSaga>(e.CorrelationId);
            saga.Transition(e);
            _repository.Save(saga, Guid.NewGuid(), null);

        }
    }
}
