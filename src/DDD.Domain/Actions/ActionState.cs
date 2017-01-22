using DDD.Common.Domain;
using DDD.Common;
using System.Collections.Generic;

namespace DDD.Domain.Actions
{
    public class ActionState: ValueObject
    {     
        public string Value { get; private set; }

        protected ActionState() { }

        protected ActionState(string value, bool validate)
        {
            if (validate)
            {
                Validate(value);
            }
            Value = value;
        }

        protected virtual void Validate(string typeValue)
        {
            if(string.IsNullOrEmpty(typeValue))
            {
                throw new FailureResult("Action state value is invalid.");
            }
        }

        public ActionState(string type): this(type, true)
        {
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return this.Value;
        }
    }
}
