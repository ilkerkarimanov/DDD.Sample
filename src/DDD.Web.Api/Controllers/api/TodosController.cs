using Microsoft.AspNetCore.Mvc;
using DDD.Core.Cqs.Query;
using DDD.Core.Cqs.Command;
using System.Threading.Tasks;
using DDD.App.Cqs.Commands.Todos;
using DDD.App.Cqs.Queries.Todos;
using DDD.Core;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace DDD.Web.Api.Controllers.api
{
    [Route("api/[controller]")]
    public class TodosController : BaseController
    {
        private readonly IQueryProcessor _queryProcessor;
        private readonly ICommandDispatcher _commandDispatcher;

        public TodosController(
            IQueryProcessor queryProcessor,
            ICommandDispatcher commandDispatcher)
        {
            _queryProcessor = queryProcessor;
            _commandDispatcher = commandDispatcher;
        }

        [HttpGet()]
        [Route("~/api/todos/{id}")]
        public async Task<IActionResult> Get(TodoByIdQuery query)
        {
            if (!ModelState.IsValid)
            {
                return ErrorModelResult();
            }
            try
            {
                var result = await _queryProcessor.ProcessAsync(query);
                return OkResult(result);
            }
            catch (FailureResult reqEx)
            {
                return ErrorResult(reqEx);
            }
        }

        [HttpGet()]
        [Route("~/api/todos")]
        public async Task<IActionResult> All(AllTodosQuery query)
        {
            if (!ModelState.IsValid)
            {
                return ErrorModelResult();
            }
            try
            {
                var result = await _queryProcessor.ProcessAsync(query);
                return OkResult(result);
            }
            catch (FailureResult reqEx)
            {
                return ErrorResult(reqEx);
            }
        }


        [HttpPost]
        [Route("~/api/todos")]
        public async Task<IActionResult> Post([FromBody]CreateTodoCommand command)
        {
            if (!ModelState.IsValid)
            {
                return ErrorModelResult();
            }
            try
            {
                var result = await _commandDispatcher.DispatchAsync<CreateTodoCommand,Result>(command);
                return ToResult(result);
            }
            catch (FailureResult reqEx)
            {
                return ErrorResult(reqEx);
            }
        }
        

        [HttpPut("{id}")]
        [Route("~/api/todos")]
        public async Task<IActionResult> Put([FromBody]UpdateTodoCommand command)
        {
            if (!ModelState.IsValid)
            {
                return ErrorModelResult();
            }
            try
            {
               var result = await _commandDispatcher.DispatchAsync<UpdateTodoCommand,Result>(command);
                return ToResult(result);
            }
            catch (FailureResult reqEx)
            {
                return ErrorResult(reqEx);
            }
        }

        [HttpPost]
        [Route("~/api/todos/{id}/complete")]
        public async Task<IActionResult> Complete([FromBody]CompleteTodoCommand command)
        {
            if (!ModelState.IsValid)
            {
                return ErrorModelResult();
            }
            try
            {
                var result = await _commandDispatcher.DispatchAsync<CompleteTodoCommand, Result>(command);
                return ToResult(result);
            }
            catch (FailureResult reqEx)
            {
                return ErrorResult(reqEx);
            }
        }

        [HttpPost]
        [Route("~/api/todos/{id}/start")]
        public async Task<IActionResult> Start([FromBody]StartTodoCommand command)
        {
            if (!ModelState.IsValid)
            {
                return ErrorModelResult();
            }
            try
            {
                var result = await _commandDispatcher.DispatchAsync<StartTodoCommand, Result>(command);
                return ToResult(result);
            }
            catch (FailureResult reqEx)
            {
                return ErrorResult(reqEx);
            }
        }


    }
}
