using System;
using CommonDomain.Core;
using CommonDomain.Persistence.EventStore;
using MemBus;
using MemBus.Configurators;
using MemBus.Subscribing;
using NEventStore;
using NEventStoreExample.Infrastructure;
using NUnit.Framework;

namespace NEventStoreExample.Test
{
    [TestFixture]
    public class saga_example
    {
        private readonly string _bucketId = (Guid.NewGuid().ToString());
        private readonly string _correlationId =(Guid.NewGuid().ToString());
        private static IStoreEvents _store;

        [Test]
        public void SetUp()
        {
            var bus = BusSetup.StartWith<Conservative>()
                .Apply<FlexibleSubscribeAdapter>(a =>
                {
                    a.ByInterface(typeof(IEventHandler<>));
                    a.ByInterface(typeof(ICommandHandler<>));
                }).Construct();             

            using (_store = WireupEventStore(bus))
            {
                var sagaRepository = new SagaEventStoreRepository(_store, new SagaFactory());

                var sut = sagaRepository.GetById<TestSaga>(_bucketId, _correlationId);
                
                sut.Transition(new TestMessage(_correlationId));
            }

        }

        private static IStoreEvents WireupEventStore(IBus bus)
        {
            return Wireup.Init()
                .UsingInMemoryPersistence()
                    .UsingJsonSerialization()
                        .Compress()
                .Build();
        }
    }

    public class TestSaga:SagaBase<TestMessage>
    {
        private TestSaga(string id)
        {
            Id = id;
            this.Register<TestMessage>(Transition);
        }

        public new string Id { get; private set; }

        private void Transition(TestMessage message)
        {

        }
    }

    public class TestMessage
    {
        public TestMessage(string correlationId)
        {
            CorrelationId = correlationId;
        }
        public string CorrelationId { get; private set; }
    }


}
