using System.Collections.Generic;
using System.Threading.Tasks;

namespace DDD.Core.Cqs.Command
{
    public interface ICommandDispatcher
    {
        Task<TReturn> DispatchAsync<TCommand,TReturn>(TCommand command);
    }
}
