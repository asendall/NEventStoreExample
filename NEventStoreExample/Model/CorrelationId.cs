using System;

namespace NEventStoreExample.Model
{
    public class CorrelationId
    {
        public CorrelationId(Guid value)
        {
            Value = value;
        }

        public Guid Value { get; private set; }
    }
}
