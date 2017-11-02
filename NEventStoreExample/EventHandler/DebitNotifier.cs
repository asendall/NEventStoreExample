using System;
using NEventStoreExample.Event;
using NEventStoreExample.Infrastructure;

namespace NEventStoreExample.EventHandler
{
    public class DebitNotifier : IEventHandler<AccountDebitedEvent>
    {
        public void Handle(AccountDebitedEvent e)
        {
            Console.WriteLine("Account debited");
        }
    }
}
