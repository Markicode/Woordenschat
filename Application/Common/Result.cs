using Application.Enums;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common
{
    public class Result<T>
    {
        public bool IsSuccess { get; }
        public T? Value { get; }
        public ErrorType? ErrorType { get; }
        public string? ErrorMessage { get; }

        private Result(bool isSuccess, T? value, ErrorType? errorType, string? errorMessage)
        {
            IsSuccess = isSuccess;
            Value = value;
            ErrorType = errorType;
            ErrorMessage = errorMessage;
        }

        public static Result<T> Success(T value)
        {
            return new Result<T>(true, value, null ,null);
        }

        public static Result<T> Failure(ErrorType errorType, string errorMessage)
        {
            return new Result<T>(false, default, errorType, errorMessage);
        }
    }
}
