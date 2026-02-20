using Application.Common;
using Application.Enums;
using Data;
using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
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
                return Result<Book>.Failure(ErrorType.NotFound, "Book not found.");
            }
            return Result<Book>.Success(book);
        }


        public async Task<Result<Book>> CreateBookAsync(CreateBookCommand createBookCommand)
        {
            Isbn? isbn = null;

            if(createBookCommand.Isbn is not null)
            {
                try
                {
                    isbn = new Isbn(createBookCommand.Isbn);
                }
                catch (InvalidOperationException ex)
                {
                    return Result<Book>.Failure(ErrorType.ValidationError, ex.Message);
                }

                var exists = await _context.Books
                    .AnyAsync(b => b.Isbn == isbn);

                if (exists)
                {
                    return Result<Book>.Failure(ErrorType.Conflict, "A book with the same ISBN already exists.");
                }
            }

            var genresResult = await LoadGenres(createBookCommand.GenreIds);
            if (!genresResult.IsSuccess)
                return Result<Book>.Failure(genresResult.ErrorType, genresResult.ErrorMessage!);

            var authorsResult = await LoadAuthors(createBookCommand.AuthorIds);
            if (!authorsResult.IsSuccess)
                return Result<Book>.Failure(authorsResult.ErrorType, authorsResult.ErrorMessage!);

            var genres = genresResult.Value!;
            var authors = authorsResult.Value!;

            Book? book = null;

            try
            {
                book = new Book(createBookCommand.Title, createBookCommand.PublishedDate, isbn, createBookCommand.Description, genres, authors);
            }
            catch (InvalidOperationException ex)
            {
                return Result<Book>.Failure(ErrorType.ValidationError, ex.Message);
            }
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return Result<Book>.Success(book);

           
        }

        public async Task<Result<Book>> ReplaceBookAsync(ReplaceBookCommand replaceBookCommand)
        {
            var book = await _context.Books
                .Include(b => b.Authors)
                .Include(b => b.Genres)
                .FirstOrDefaultAsync(b => b.Id == replaceBookCommand.BookId);

            if (book == null)
            {
                return Result<Book>.Failure(ErrorType.NotFound, "Book not found.");
            }

            Isbn? isbn = null;

            if (replaceBookCommand.Isbn is not null)
            {
                try
                {
                    isbn = new Isbn(replaceBookCommand.Isbn);
                }
                catch (InvalidOperationException ex)
                {
                    return Result<Book>.Failure(ErrorType.ValidationError, ex.Message);
                }

                var exists = await _context.Books
                    .AnyAsync(b => b.Id != book.Id && b.Isbn == isbn);

                if (exists)
                {
                    return Result<Book>.Failure(ErrorType.Conflict, "A book with the same ISBN already exists.");
                }
            }

            var genresResult = await LoadGenres(replaceBookCommand.GenreIds);
            if (!genresResult.IsSuccess)
                return Result<Book>.Failure(genresResult.ErrorType, genresResult.ErrorMessage!);

            var authorsResult = await LoadAuthors(replaceBookCommand.AuthorIds);
            if (!authorsResult.IsSuccess)
                return Result<Book>.Failure(authorsResult.ErrorType, authorsResult.ErrorMessage!);

            var genres = genresResult.Value!;
            var authors = authorsResult.Value!;
            try
            {
                book.ReplaceDetails(replaceBookCommand.Title, replaceBookCommand.PublishedDate, isbn, replaceBookCommand.Description, genres, authors);
                await _context.SaveChangesAsync();
                return Result<Book>.Success(book);
            }
            catch (InvalidOperationException ex)
            {
                return Result<Book>.Failure(ErrorType.ValidationError, ex.Message);
            }
        }

        public async Task<Result<Unit>> DeleteBookAsync(int bookId)
        {
            var book = await _context.Books.FindAsync(bookId);
            if (book == null)
            {
                return Result<Unit>.Failure(ErrorType.NotFound, "Book not found.");
            }
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return Result<Unit>.Success(Unit.Value);
        }

        public async Task<Result<Book>> PatchBookAsync(PatchBookCommand patchBookCommand)
        {
            var book = await _context.Books
                .Include(b => b.Authors)
                .Include(b => b.Genres)
                .FirstOrDefaultAsync(b => b.Id == patchBookCommand.BookId);
            if (book == null)
            {
                return Result<Book>.Failure(ErrorType.NotFound, "Book not found.");
            }

            // Update only the fields that are provided
            if (patchBookCommand.Title != null)
                book.UpdateTitle(patchBookCommand.Title);

            if (patchBookCommand.Isbn.HasValue)
            {
                Isbn? isbn = null;
                if (patchBookCommand.Isbn.Value != null)
                {
                    try
                    {
                        isbn = new Isbn(patchBookCommand.Isbn.Value);
                    }
                    catch (InvalidOperationException ex)
                    {
                        return Result<Book>.Failure(ErrorType.ValidationError, ex.Message);
                    }

                    var exists = await _context.Books
                        .AnyAsync(b => b.Id != book.Id && b.Isbn == isbn);

                    if (exists)
                    {
                        return Result<Book>.Failure(ErrorType.Conflict, "A book with the same ISBN already exists.");
                    }
                }
                book.UpdateIsbn(isbn);
            }
            if (patchBookCommand.Description.HasValue)
            {
                book.UpdateDescription(patchBookCommand.Description.Value);
            }

            if (patchBookCommand.PublishedDate.HasValue)
            {
                book.UpdatePublishedDate(patchBookCommand.PublishedDate.Value);
            }

            if (patchBookCommand.GenreIds != null)
            {
                var genresResult = await LoadGenres(patchBookCommand.GenreIds);
                if (!genresResult.IsSuccess)
                    return Result<Book>.Failure(genresResult.ErrorType, genresResult.ErrorMessage!);

                var genres = genresResult.Value!;
                book.UpdateGenres(genres);
            }

            if (patchBookCommand.AuthorIds != null)
            {
                var authorsResult = await LoadAuthors(patchBookCommand.AuthorIds);
                if (!authorsResult.IsSuccess)
                    return Result<Book>.Failure(authorsResult.ErrorType, authorsResult.ErrorMessage!);

                var authors = authorsResult.Value!;
                book.UpdateAuthors(authors);
            }
            

            await _context.SaveChangesAsync();
            return Result<Book>.Success(book);
        }

        private async Task<Result<List<Author>>> LoadAuthors(IEnumerable<int> authorIds)
        {
            if (authorIds == null || !authorIds.Any())
                return Result<List<Author>>.Failure(ErrorType.ValidationError, "At least one author ID must be provided.");

            var distinctIds = authorIds.Distinct().ToList();

            var authors = await _context.Authors
                .Where(a => distinctIds.Contains(a.Id))
                .ToListAsync();

            if (authors.Count != distinctIds.Count)
                return Result<List<Author>>.Failure(ErrorType.ValidationError, "One or more author IDs are invalid.");

            return Result<List<Author>>.Success(authors);
        }

        private async Task<Result<List<Genre>>> LoadGenres(IEnumerable<int> genreIds)
        {
            if (genreIds == null || !genreIds.Any())
                return Result<List<Genre>>.Failure(ErrorType.ValidationError, "At least one genre ID must be provided.");

            var distinctIds = genreIds.Distinct().ToList();

            var genres = await _context.Genres
                .Where(g => distinctIds.Contains(g.Id))
                .ToListAsync();

            if (genres.Count != distinctIds.Count)
                return Result<List<Genre>>.Failure(ErrorType.ValidationError, "One or more genre IDs are invalid.");

            return Result<List<Genre>>.Success(genres);
        }
    }
    
}
