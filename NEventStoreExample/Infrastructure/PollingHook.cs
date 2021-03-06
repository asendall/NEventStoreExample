﻿using NEventStore;
using NEventStore.Client;

namespace NEventStoreExample.Infrastructure
{
    internal class PollingHook: PipelineHookBase
    {
        private readonly IObserveCommits _commitsObserver;

        public PollingHook(IObserveCommits commitsObserver)
        {
            _commitsObserver = commitsObserver;
        }

        public override void PostCommit(ICommit committed)
        {
            base.PostCommit(committed);
            _commitsObserver.PollNow();
        }
    }
}
