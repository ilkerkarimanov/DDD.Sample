using DDD.Core.Cqs.Command;
using System.Threading.Tasks;
using DDD.Core;
using DDD.Domain.Todos;
using DDD.App.Cqs.Commands.Todos;

namespace DDD.App.Cqs.CommandHandlers.Todos
{
    public partial class TodosCommandHandler:
          IAsyncCommandHandler<CreateTodoCommand, Result>,
          IAsyncCommandHandler<UpdateTodoCommand, Result>
    {
        private readonly ITodoFinder _todoFinder;
        private readonly ITodoRepository _todoRepository;

        public TodosCommandHandler(
            ITodoFinder todoFinder,
            ITodoRepository todoRepository)
        {
            _todoFinder = todoFinder;
            _todoRepository = todoRepository;
        }

        public async Task<Result> HandleAsync(CreateTodoCommand command)
        {
            var todo = await CreateTodo(command);

            await _todoRepository.Create(todo);

            return await Task.FromResult(Result.Ok());
        }

        public async Task<Result> HandleAsync(UpdateTodoCommand command)
        {
            var todoResult = await _todoFinder.GetById(new TodoId(command.Id));
            if (todoResult.HasNoValue)
            {
                var message = $"Todo does not exists in the system.";
                return await Task.FromResult(Result.Fail(message));
            }
            if(todoResult.Value.State == TodoState.Completed)
            {
                var message = $"Todo have been already completed - modification is not allowed.";
                return await Task.FromResult(Result.Fail(message));
            }
            var todo = await MapEditCommandToTodo(command, todoResult.Value);
            
            await _todoRepository.Update(todo);

            return await Task.FromResult(Result.Ok());
        }
        private async Task<Todo> CreateTodo(CreateTodoCommand command)
        {
            return await Task.FromResult(CreateTodoFactory.Create(command));
        }
        private async Task<Todo> MapEditCommandToTodo(
                UpdateTodoCommand command,
                Todo todo)
        {
            return await Task.FromResult(UpdateTodoFactory.CreateFrom(command, todo));
        }
    }
}
