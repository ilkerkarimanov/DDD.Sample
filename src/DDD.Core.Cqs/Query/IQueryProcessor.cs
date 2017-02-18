using System.Threading.Tasks;

namespace DDD.Core.Cqs.Query
{
    public interface IQueryProcessor
    {
        Task<TResult> ProcessAsync<TResult>(IQuery<TResult> query);
    }
}