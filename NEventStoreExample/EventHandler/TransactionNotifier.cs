using System;
using NEventStoreExample.Event;
using NEventStoreExample.Infrastructure;

namespace NEventStoreExample.EventHandler
{
    public class TransactionNotifier : IEventHandler<TransactionCreatedEvent>
    {
        public void Handle(TransactionCreatedEvent e)
        {
            Console.WriteLine("Transaction requested");
        }
    }
}
