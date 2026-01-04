using Application.Enums;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common
{
    public class Result
    {
        public bool IsSuccess { get; }
        public ErrorType? ErrorType { get; }
        public string? ErrorMessage { get; }

        protected Result(bool isSuccess, ErrorType? errorType, string? errorMessage)
        {
            IsSuccess = isSuccess;
            ErrorType = errorType;
            ErrorMessage = errorMessage;
        }

        public static Result Success()
        {
            return new Result(true, null ,null);
        }

        public static Result Failure(ErrorType errorType, string errorMessage)
        {
            return new Result(false, errorType, errorMessage);
        }
    }
}
