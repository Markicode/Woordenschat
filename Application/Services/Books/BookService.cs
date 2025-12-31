using Application.Common;
using Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Application.Services.Books
{
    public class BookService : IBookService
    {
        private readonly LibraryContext _context;

        public BookService(LibraryContext context)
        {
            _context = context;
        }

        public async Task<Result<List<Book>>> GetBooksAsync()
        {

            var books = await _context.Books
                .Include(b => b.Authors)
                .Include(b => b.Genres)
                .ToListAsync();

            return Result<List<Book>>.Success(books);
        }

        public async Task<Result<Book>> GetBookByIdAsync(int id)
        {
            var book = await _context.Books
                .Include(b => b.Authors)
                .Include(b => b.Genres)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book == null)
            {
                return Result<Book>.Failure("Book not found.");
            }
            return Result<Book>.Success(book);
        }


        public async Task<Result<Book>> CreateBookAsync(CreateBookCommand createBookCommand)
        {
            var genreIds = createBookCommand.GenreIds.Distinct().ToList();
            var authorIds = createBookCommand.AuthorIds.Distinct().ToList();

            var genres = await _context.Genres
            .Where(g => genreIds.Contains(g.Id))
            .ToListAsync();

            var authors = await _context.Authors
                .Where(a => authorIds.Contains(a.Id))
                .ToListAsync();

            if(authors.Count != authorIds.Count)
            {
                return Result<Book>.Failure("One or more author IDs are invalid.");
            }

            if (genres.Count != genreIds.Count)
            {
                return Result<Book>.Failure("One or more genre IDs are invalid.");
            }

            var book = new Book
            {
                Title = createBookCommand.Title,
                Description = createBookCommand.Description,
                Isbn = createBookCommand.Isbn,
                PublishedDate = createBookCommand.PublishedDate,
                Genres = genres,
                Authors = authors
            };
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return Result<Book>.Success(book);

           
        }

        public async Task<Result<Book>> ReplaceBookAsync(ReplaceBookCommand replaceBookCommand)
        {
            var genreIds = replaceBookCommand.GenreIds.Distinct().ToList();
            var authorIds = replaceBookCommand.AuthorIds.Distinct().ToList();

            var genres = await _context.Genres
            .Where(g => genreIds.Contains(g.Id))
            .ToListAsync();

            var authors = await _context.Authors
                .Where(a => authorIds.Contains(a.Id))
                .ToListAsync();

            if (authors.Count != authorIds.Count)
            {
                return Result<Book>.Failure("One or more author IDs are invalid.");
            }

            if (genres.Count != genreIds.Count)
            {
                return Result<Book>.Failure("One or more genre IDs are invalid.");
            }

            var book = await _context.Books
                .Include(b => b.Authors)
                .Include(b => b.Genres)
                .FirstOrDefaultAsync(b => b.Id == replaceBookCommand.BookId);

            if (book == null)
            {
                return Result<Book>.Failure("Book not found.");
            }

            book.Title = replaceBookCommand.Title;
            book.Isbn = replaceBookCommand.Isbn;
            book.Description = replaceBookCommand.Description;
            book.PublishedDate = replaceBookCommand.PublishedDate;

            // Replace relations
            book.Authors.Clear();
            foreach (var author in authors)
                book.Authors.Add(author);

            book.Genres.Clear();
            foreach (var genre in genres)
                book.Genres.Add(genre);

            await _context.SaveChangesAsync();

            return Result<Book>.Success(book);
        }
    }
    
}
