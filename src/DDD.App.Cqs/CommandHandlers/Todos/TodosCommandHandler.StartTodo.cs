using DDD.Core.Cqs.Command;
using System.Threading.Tasks;
using DDD.Core;
using DDD.Domain.Todos;
using DDD.App.Cqs.Commands.Todos;

namespace DDD.App.Cqs.CommandHandlers.Todos
{
    public partial class TodosCommandHandler:
          IAsyncCommandHandler<StartTodoCommand, Result>
    {
        public async Task<Result> HandleAsync(StartTodoCommand command)
        {
            var todoResult = await _todoFinder.GetById(new TodoId(command.Id));
            if (todoResult.HasNoValue)
            {
                var message = $"Todo does not exists in the system.";
                return await Task.FromResult(Result.Fail(message));
            }

            var todo = todoResult.Value;
            var action = await CreateStartAction(command);

            todo.ApplyAction(action);

            await _todoRepository.Update(todo);

            return await Task.FromResult(Result.Ok());
        }
        
        private async Task<TodoAction> CreateStartAction(StartTodoCommand command)
        {
            return await Task.FromResult(StartTodoFactory.Create(command));
        }

    }
}
