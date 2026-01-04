using Application.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common
{
    public class Result<T> : Result
    {
        public T? Value { get; }

        private Result(bool isSuccess, T? value, ErrorType? errorType, string? errorMessage)
            : base(isSuccess, errorType, errorMessage)
        {
            Value = value;
        }

        public static Result<T> Success(T value)
        {
            return new Result<T>(true, value, null, null);
        }

        public static new Result<T> Failure(ErrorType errorType, string errorMessage)
        {
            return new Result<T>(false, default, errorType, errorMessage);
        }
    }
    
    
}
