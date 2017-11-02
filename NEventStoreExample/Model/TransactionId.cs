using System;

namespace NEventStoreExample.Model
{
    public class TransactionId
    {
        public TransactionId(Guid value)
        {
            Value = value;
        }

        public Guid Value { get; private set; }
    }
}
