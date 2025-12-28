using Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Books
{
    public class BookService : IBookService
    {
        private readonly LibraryContext _context;

        public BookService(LibraryContext context)
        {
            _context = context;
        }

        public async Task<GetBooksResult> GetBooksAsync()
        {

            var books = await _context.Books
                .Include(b => b.Authors)
                .Include(b => b.Genres)
                .ToListAsync();

            return GetBooksResult.Success(books);
        }

        public async Task<GetBookByIdResult> GetBookByIdAsync(int id)
        {
            var book = await _context.Books
                .Include(b => b.Authors)
                .Include(b => b.Genres)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book == null)
            {
                return GetBookByIdResult.Failure("Book not found.");
            }
            return GetBookByIdResult.Success(book);
        }


        public async Task<CreateBookResult> CreateBookAsync(CreateBookCommand command)
        {
            var genreIds = command.GenreIds.Distinct().ToList();
            var authorIds = command.AuthorIds.Distinct().ToList();

            var genres = await _context.Genres
            .Where(g => genreIds.Contains(g.Id))
            .ToListAsync();

            var authors = await _context.Authors
                .Where(a => authorIds.Contains(a.Id))
                .ToListAsync();

            if(authors.Count != authorIds.Count)
            {
                return CreateBookResult.Failure("One or more author IDs are invalid.");
            }

            if (genres.Count != genreIds.Count)
            {
                return CreateBookResult.Failure("One or more genre IDs are invalid.");
            }

            var book = new Book
            {
                Title = command.Title,
                Description = command.Description,
                Isbn = command.Isbn,
                PublishedDate = command.PublishedDate,
                Genres = genres,
                Authors = authors
            };
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return CreateBookResult.Success(book);

           
        }
    }
    
}
