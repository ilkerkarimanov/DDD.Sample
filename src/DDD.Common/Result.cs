using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.Common
{
    public class Result: IResult
    {

        public bool Succeeded { get; internal set; }
        public IEnumerable<string> Errors { get; internal set; }

        protected Result(params string[] errors) : this((IEnumerable<string>)errors)
        {
        }

        protected Result(IEnumerable<string> errors)
        {
            if (errors == null)
            {
                new ArgumentNullException(nameof(errors));
            }

            Succeeded = false;
            Errors = errors;
        }

        protected Result(bool success)
        {
            Succeeded = success;
            Errors = new string[0];
        }

        public override string ToString()
        {
            var errors = new StringBuilder();
            foreach (var error in Errors)
            {
                errors.AppendLine(" > " + error);
            }
            return (Succeeded ? "Succeeded" : "Failed:") + errors;
        }
        
        public static Result Ok()
        {
            return new Result(true);
        }
        public static Result Fail(string[] errors)
        {
            return new Result(errors);
        }

        public static Result Fail(string error)
        {
            return new Result(new string[1] { error });
        }

        public static Result Fail<T> (T value, string error)
        {
            return new Result<T>(value, error);
        }

        public static Result Ok<T>(T value)
        {
            return new Result<T>(value);
        }
        
    }

    public class Result<T> : Result
    {
        private readonly T _value;
        public T Value
        {
            get
            {
                if (!Succeeded)
                    throw new InvalidOperationException();

                return _value;
            }
        }

        protected internal Result(T value, string error)
            : base(error)
        {
            _value = value;
        }

        protected internal Result(T value): base(true)
        {
            _value = value;
        }
    }
}
