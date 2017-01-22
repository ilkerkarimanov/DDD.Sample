using DDD.Common.Domain;

namespace DDD.Domain.Todos
{
    public interface ITodoRepository: IRepository<Todo, TodoId>
    {
    }
}
