using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Books
{
    public interface IBookService
    {
        Task<CreateBookResult> CreateBookAsync(CreateBookCommand command);

        Task<GetBooksResult> GetBooksAsync();

        Task<GetBookByIdResult> GetBookByIdAsync(int bookId);
    }
}
