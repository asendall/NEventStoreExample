using System;
using System.Collections.Generic;
using System.Linq;
using CommonDomain;
using CommonDomain.Persistence;
using NEventStore;

namespace NEventStoreExample.Test
{
    public class InMemoryEventRepository : IRepository
    {
        private readonly IConstructAggregates aggregateFactory;

        private readonly List<Infrastructure.Event> givenEvents;

        public InMemoryEventRepository(List<Infrastructure.Event> givenEvents, IConstructAggregates aggregateFactory)
        {
            this.givenEvents = givenEvents;
            this.aggregateFactory = aggregateFactory;
        }

        public List<Infrastructure.Event> Events { get; private set; }

        public TAggregate GetById<TAggregate>(Guid id) where TAggregate : class, IAggregate
        {
            return GetById<TAggregate>(Bucket.Default, id, 0);
        }

        public TAggregate GetById<TAggregate>(Guid id, int version) where TAggregate : class, IAggregate
        {
            return GetById<TAggregate>(Bucket.Default, id, 0);
        }

        public TAggregate GetById<TAggregate>(string bucketId, Guid id) where TAggregate : class, IAggregate
        {
            return GetById<TAggregate>(Bucket.Default, id, 0);
        }

        public TAggregate GetById<TAggregate>(string bucketId, Guid id, int version) where TAggregate : class, IAggregate
        {
            var aggregate = aggregateFactory.Build(typeof(TAggregate), id, null);
            givenEvents.ForEach(aggregate.ApplyEvent);

            return (TAggregate)aggregate;
        }

        public void Save(IAggregate aggregate, Guid commitId, Action<IDictionary<string, object>> updateHeaders)
        {
            Save(Bucket.Default, aggregate, commitId, updateHeaders);
        }

        public void Save(string bucketId, IAggregate aggregate, Guid commitId, Action<IDictionary<string, object>> updateHeaders)
        {
            Events = aggregate.GetUncommittedEvents().Cast<Infrastructure.Event>().ToList();
        }

        public void Dispose()
        {
            // no op
        }
    }
}