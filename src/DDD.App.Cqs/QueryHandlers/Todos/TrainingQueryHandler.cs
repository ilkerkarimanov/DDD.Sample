using DDD.App.Cqs.Queries.Todos;
using DDD.App.Cqs.QueryResult.Todos;
using DDD.Core;
using DDD.Core.Cqs.Query;
using DDD.Domain.Todos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDD.App.Cqs.QueryHandlers.Todos
{
    public class TodoQueryHandler :
        IHandleQueryAsync<TodoByIdQuery, TodoResult>,
        IHandleQueryAsync<AllTodosQuery, IEnumerable<TodoResult>>
    {
        private readonly ITodoFinder _todoFinder;

        public TodoQueryHandler(
            ITodoFinder todoFinder
            )
        {
            _todoFinder = todoFinder;
        }

        public async Task<TodoResult> ExecuteAsync(TodoByIdQuery query)
        {
            var todo = await _todoFinder.GetById(new TodoId(query.Id));
            return await GetResult(todo);

        }

        public async Task<IEnumerable<TodoResult>> ExecuteAsync(AllTodosQuery query)
        {
            var todos = await _todoFinder.All();
            return await Task.Factory.StartNew(() =>
            {
                return todos.Select(x => GetResult(x).Result);
            });

        }
        private async Task<TodoResult> GetResult(Maybe<Todo> todo)
        {
            return await Task.Factory.StartNew(() =>
            {
                if (todo.HasNoValue) return default(TodoResult);
                return TodoResultFactory.Create(todo.Value);
            });
        }
    }
}
