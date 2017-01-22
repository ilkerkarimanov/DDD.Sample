using DDD.Common.Domain;
using DDD.Domain.Actions;
using DDD.Common;
using System.Collections.Generic;

namespace DDD.Domain.Todos
{
    public class TodoState: ActionState
    {
        public static readonly TodoState Pending = new TodoState("Pending", false);
        public static readonly TodoState InProgress = new TodoState("In Progress", false);
        public static readonly TodoState Completed = new TodoState("Completed", false);
        protected TodoState(): base() { }
        private TodoState(string value, bool validate): base(value, validate) { }
        public TodoState(string type) : base(type, true) { }
        protected override void Validate(string typeValue)
        {
            if(!(typeValue.Equals(Pending.Value) ||
                typeValue.Equals(InProgress.Value) ||
                typeValue.Equals(Completed.Value)
                )) {
                throw new FailureResult("Todo state value is invalid.");
            }
        }        
    }
}
