using Application.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Services.Books
{
    public interface IBookService
    {
        Task<Result<Book>> CreateBookAsync(CreateBookCommand createBookCommand);

        Task<Result<List<Book>>> GetBooksAsync();

        Task<Result<Book>> GetBookByIdAsync(int bookId);

        Task<Result<Book>> ReplaceBookAsync(ReplaceBookCommand replaceBookCommand);

        Task<Result<Unit>> DeleteBookAsync(int bookId);

        Task<Result<Book>> PatchBookAsync(PatchBookCommand patchBookCommand);
    }
}
