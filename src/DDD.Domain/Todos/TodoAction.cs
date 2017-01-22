using DDD.Common.Domain;
using DDD.Domain.Actions;

namespace DDD.Domain.Todos
{
    public class TodoAction: Action<TodoState>
    {

        public TodoAction(
            TodoState state) : base(state) {
        }
    }
    
}
