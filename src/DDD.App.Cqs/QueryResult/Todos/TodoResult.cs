using DDD.Domain.Todos;
using System.Linq;

namespace DDD.App.Cqs.QueryResult.Todos
{
    public class TodoResult
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public string State { get; set; }
        public bool IsDone { get; set; }

    }

    public static class TodoResultFactory
    {

        public static TodoResult Create(Todo todo)
        {
            var result = new TodoResult()
            {
                Id = todo.Id.Id,
                Description = todo.Description,
                State = todo.State.Value,
                IsDone = todo.IsDone
            };
            return result;
        }
    }

}
