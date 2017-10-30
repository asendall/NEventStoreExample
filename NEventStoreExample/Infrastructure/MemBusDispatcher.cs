using System;
using MemBus;
using NEventStore;

namespace NEventStoreExample.Infrastructure
{
    internal class MemBusDispatcher : IObserver<ICommit>
    {
        private readonly IBus _bus;

        public MemBusDispatcher(IBus bus)
        {
            _bus = bus;
        }
        public void OnNext(ICommit commit)
        {
            foreach (var @event in commit.Events)
            {
                _bus.Publish(@event.Body);
            }
        }

        public void OnError(Exception error)
        {
            //throw new NotImplementedException();
        }

        public void OnCompleted()
        {
            //throw new NotImplementedException();
        }
    }
}
