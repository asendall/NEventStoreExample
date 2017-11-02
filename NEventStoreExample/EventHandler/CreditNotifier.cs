using System;
using NEventStoreExample.Event;
using NEventStoreExample.Infrastructure;

namespace NEventStoreExample.EventHandler
{
    public class CreditNotifier : IEventHandler<AccountCreditedEvent>
    {
        public void Handle(AccountCreditedEvent e)
        {
            Console.WriteLine("Account credited");
        }
    }
}
