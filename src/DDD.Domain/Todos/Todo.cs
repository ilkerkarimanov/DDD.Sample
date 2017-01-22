using DDD.Common.Domain;
using DDD.Common;
using System.Collections.Generic;
using System.Linq;

namespace DDD.Domain.Todos
{
    public class Todo : Entity, IAggregateRoot
    {
        public TodoId Id { get; private set; }
        public string Description { get; private set; }
        public bool IsDone
        {
            get
            {
                return this.State == TodoState.Completed ? true : false;
            }
        }

        protected Todo()
        {
            _actions = new List<TodoAction>();
        }

        public Todo(
            TodoId id,
            string description) : this()
        {
            this.Id = id;
            ChangeDescrition(description);
            _actions.Add(new TodoAction(
                TodoState.Pending));
        }

        public void ChangeDescrition(string description)
        {
            if (string.IsNullOrEmpty(description))
                throw new FailureResult("Description cannot be empty.");
            this.Description = description;
        }

        private IList<TodoAction> _actions;
        public IReadOnlyList<TodoAction> Actions
        {
            get
            {
                return _actions.ToList();
            }
            set
            {
                _actions = value.ToList();
            }
        }
        public TodoState State
        {
            get
            {
                return _actions.Last().State;
            }
        }
        public void ApplyAction(TodoAction action)
        {
            if (State == TodoState.Completed)
            {
                throw new FailureResult("Todos have been already completed.");
            }
            else if (State == TodoState.InProgress)
            {
                if (action.State != TodoState.Completed)
                    throw new FailureResult("Invalid state transition is requested by the action.");
            }
            _actions.Add(action);
        }

        protected override IEnumerable<object> GetIdentityComponents()
        {
            yield return this.Id;
            yield return this.Description;
            yield return this.IsDone;
            yield return this.State;
        }
    }
}
