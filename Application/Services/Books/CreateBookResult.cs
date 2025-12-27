using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Services.Books
{
    public class CreateBookResult
    {
        public bool IsSuccess { get; set; }
        public Book? Book { get; set; }
        public string? ErrorMessage { get; set; }

        private CreateBookResult(bool isSuccess, Book? book, string? errorMessage)
        {
            IsSuccess = isSuccess;
            Book = book;
            ErrorMessage = errorMessage;
        }

        public static CreateBookResult Success(Book book)
        {
            return new CreateBookResult(true, book, null);
        }

        public static CreateBookResult Failure(string errorMessage)
        {
            return new CreateBookResult(false, null, errorMessage);
        }
    }
}
