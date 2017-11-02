using System;
using NEventStore;
using NEventStore.Dispatcher;

namespace NEventStoreExample.Infrastructure
{
    internal class CommitObserver : IObserver<ICommit>
    {
        private readonly IDispatchCommits _dispatcher;


        public CommitObserver(IDispatchCommits dispatcher)
        {
            _dispatcher = dispatcher;
        }
        public void OnNext(ICommit commit)
        {
            _dispatcher.Dispatch(commit);
        }

        public void OnError(Exception error)
        {
            Console.WriteLine("Exception from ReadModelCommitObserver: {0}", error.Message);
            throw error;
        }

        public void OnCompleted()
        {
            Console.WriteLine("commit observation completed.");
        }
    }
}
