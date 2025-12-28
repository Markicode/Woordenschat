using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Books
{
    public class GetBookByIdResult
    {
        public bool IsSuccess { get; set; }
        public Book? Book { get; set; }
        public string? ErrorMessage { get; set; }

        private GetBookByIdResult(bool isSuccess, Book? book, string? errorMessage)
        {
            IsSuccess = isSuccess;
            Book = book;
            ErrorMessage = errorMessage;
        }

        public static GetBookByIdResult Success(Book book)
        {
            return new GetBookByIdResult(true, book, null);
        }

        public static GetBookByIdResult Failure(string errorMessage)
        {
            return new GetBookByIdResult(false, null, errorMessage);
        }
    }

}
