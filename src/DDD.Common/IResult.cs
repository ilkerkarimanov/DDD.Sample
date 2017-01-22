using System.Collections.Generic;

namespace DDD.Common
{

    public interface IFailure
    {
        IEnumerable<string> Errors { get; }
    }
    public interface IResult: IFailure
    {
        bool Succeeded { get; }

        string ToString();
    }
}