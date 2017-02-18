using System.Threading.Tasks;


namespace DDD.Core.Cqs.Command
{
    public interface IAsyncCommandHandler
    { }

    public interface IAsyncCommandHandler<in TCommand, TResult>
    {
        Task<TResult> HandleAsync(TCommand command);
    }
}