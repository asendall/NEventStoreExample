using System;
using CommonDomain.Core;
using CommonDomain.Persistence.EventStore;
using MemBus;
using MemBus.Configurators;
using MemBus.Subscribing;
using NEventStore;
using NEventStore.Client;
using NEventStore.Dispatcher;
using NEventStoreExample.CommandHandler;
using NEventStoreExample.EventHandler;
using NEventStoreExample.Infrastructure;
using NEventStoreExample.ProcessManagers;

namespace NEventStoreExample
{
    public class Program
    {
        private static readonly Guid Account1Id = Guid.NewGuid();
        private static readonly Guid Account2Id = Guid.NewGuid();
        private static readonly Guid TransactionId = Guid.NewGuid();
        private static readonly Guid CorrelationId = Guid.NewGuid();

        private static IStoreEvents _store;
        private static IStoreEvents _sagaStore;

        public static void Main(string[] args)
        {
            var bus = BusSetup.StartWith<Conservative>()
                .Apply<FlexibleSubscribeAdapter>(a =>
                {
                    a.ByInterface(typeof(IEventHandler<>));
                    a.ByInterface(typeof(ICommandHandler<>));
                }).Construct();

            var someAwesomeUi = new SomeAwesomeUi(bus);
            using (_store = WireupEventStore(bus)) { 
            using (_sagaStore = WireupEventStore(bus))
            {
                var repository = new EventStoreRepository(_store, new AggregateFactory(), new ConflictDetector());
                var sagaRepository = new SagaEventStoreRepository(_sagaStore, new SagaFactory());

                //register handlers
                bus.Subscribe(new CreateAccountCommandHandler(repository));
                bus.Subscribe(new CloseAccountCommandHandler(repository));
                bus.Subscribe(new CreateTransactionCommandHandler(repository));
                bus.Subscribe(new CreditAccountCommandHandler(repository));
                bus.Subscribe(new DebitAccountCommandHandler(repository));
                bus.Subscribe(new KaChingNotifier());
                bus.Subscribe(new OmgSadnessNotifier());
                bus.Subscribe(new TransactionProcessManager(sagaRepository, bus));
                bus.Subscribe(new TransactionNotifier());
                bus.Subscribe(new CreditNotifier());
                bus.Subscribe(new DebitNotifier());



                var client = new PollingClient(_store.Advanced);
                var checkpointToken = LoadCheckpoint();

                using (var observeCommits = client.ObserveFrom(checkpointToken))
                using (observeCommits.Subscribe(new CommitObserver(new DelegateMessageDispatcher(commit =>
                {
                    try
                    {
                        foreach (EventMessage @event in commit.Events)
                            bus.Publish(@event.Body);
                        //Console.WriteLine("Message dispatched");
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Unable to dispatch");
                    }
                    //Console.WriteLine("Checkpoint token= " + commit.CheckpointToken);
                    checkpointToken = commit.CheckpointToken;
                }))))
                {
                    observeCommits.Start();

                    //Do something
                    someAwesomeUi.CreateNewAccount(Account1Id, "Luiz", "@luizdamim");
                    someAwesomeUi.CreateNewAccount(Account2Id, "Andrew", "@asendall");
                    someAwesomeUi.CreateNewTransaction(CorrelationId, TransactionId, Account1Id, Account2Id, 100);

                    Console.ReadKey();

                    SaveCheckpoint(checkpointToken);
                }

            }
        }
    }



        private static string LoadCheckpoint()
        {
            // Load the checkpoint value from disk / local db/ etc
            return null;
        }

        private static void SaveCheckpoint(string checkpointToken)
        {
            //Save checkpointValue to disk / whatever.
        }

        private static IStoreEvents WireupEventStore(IBus bus)
        {
            return Wireup.Init()
                .UsingInMemoryPersistence()
                    .UsingJsonSerialization()
                        .Compress()                     
                .Build();
        }

        //private static IStoreEvents WireupEventStore(IBus bus)
        //{
        //    return Wireup.Init()
        //        ////.LogToOutputWindow()
        //        ////.LogToConsoleWindow()
        //        .UsingInMemoryPersistence()
        //            .UsingJsonSerialization()
        //                .Compress()                      
        //        .UsingSynchronousDispatchScheduler()
        //        .DispatchTo(new DelegateMessageDispatcher(c => DelegateDispatcher.DispatchCommit(bus, c)))
        //        .Build();
        //}
    }
}
