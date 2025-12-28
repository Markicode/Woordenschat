using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Books
{
    public class GetBooksResult
    {
        public bool IsSuccess { get; set; }
        public List<Book>? Books { get; set; }
        public string? ErrorMessage { get; set; }

        private GetBooksResult(bool isSuccess, List<Book>? books, string? errorMessage)
        {
            IsSuccess = isSuccess;
            Books = books;
            ErrorMessage = errorMessage;
        }

        public static GetBooksResult Success(List<Book> books)
        {
            return new GetBooksResult(true, books, null);
        }

        public static GetBooksResult Failure(string errorMessage)
        {
            return new GetBooksResult(false, null, errorMessage);
        }
    }
}
