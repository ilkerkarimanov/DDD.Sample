using System;
using System.Collections.Generic;
using DDD.Common.Domain;

namespace DDD.Domain.Actions
{
    public class Action<T>: ValueObject where T: ActionState
    {
        public T State { get; private set; }

        public Action(T state)
        {
            this.State = state;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return this.State;
        }
    }
}
