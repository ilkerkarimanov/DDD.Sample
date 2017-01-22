using DDD.App.Cqs.QueryResult.Todos;
using DDD.Common.Cqs.Query;
using System.Collections.Generic;

namespace DDD.App.Cqs.Queries.Todos
{
    public class AllTodosQuery : IQuery<IEnumerable<TodoResult>> { }
}
