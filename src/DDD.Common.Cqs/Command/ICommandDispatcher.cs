using System.Collections.Generic;
using System.Threading.Tasks;

namespace DDD.Common.Cqs.Command
{
    public interface ICommandDispatcher
    {
        Task<TReturn> DispatchAsync<TCommand,TReturn>(TCommand command);
    }
}
