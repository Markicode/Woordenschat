using Application.Common;
using Application.Enums;
using Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Application.Common.Validation;

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
            if (createBookCommand.GenreIds is null || !createBookCommand.GenreIds.Any())
            { 
                return Result<Book>.Failure(ErrorType.ValidationError, "At least one genre ID must be provided.");
            }

            if (createBookCommand.AuthorIds is null || !createBookCommand.AuthorIds.Any())
            {
                return Result<Book>.Failure(ErrorType.ValidationError, "At least one author ID must be provided.");
            }

            if(createBookCommand.PublishedDate > DateOnly.FromDateTime(DateTime.Now))
            {
                return Result<Book>.Failure(ErrorType.ValidationError, "Published date cannot be in the future.");
            }

            if(!IsbnHelper.IsValid(createBookCommand.Isbn))
            {
                return Result<Book>.Failure(ErrorType.ValidationError, "Invalid ISBN format.");
            }

            if(createBookCommand.Isbn != null)
            {
                var normalizedIsbn = IsbnHelper.Normalize(createBookCommand.Isbn);

                var exists = await _context.Books
                    .AnyAsync(b => b.Isbn == normalizedIsbn);

                if(exists)
                    {
                    return Result<Book>.Failure(ErrorType.Conflict, "A book with the same ISBN already exists.");
                }
            }

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
                return Result<Book>.Failure(ErrorType.ValidationError, "One or more author IDs are invalid.");
            }

            if (genres.Count != genreIds.Count)
            {
                return Result<Book>.Failure(ErrorType.ValidationError, "One or more genre IDs are invalid.");
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
            var book = await _context.Books
                .Include(b => b.Authors)
                .Include(b => b.Genres)
                .FirstOrDefaultAsync(b => b.Id == replaceBookCommand.BookId);

            if (book == null)
            {
                return Result<Book>.Failure(ErrorType.NotFound, "Book not found.");
            }

            book.Title = replaceBookCommand.Title;
            book.Isbn = replaceBookCommand.Isbn;
            book.Description = replaceBookCommand.Description;
            book.PublishedDate = replaceBookCommand.PublishedDate;


            if (replaceBookCommand.AuthorIds is null || !replaceBookCommand.AuthorIds.Any())
                return Result<Book>.Failure(ErrorType.ValidationError, "At least one author is required.");

            if (replaceBookCommand.GenreIds is null || !replaceBookCommand.GenreIds.Any())
                return Result<Book>.Failure(ErrorType.ValidationError, "At least one genre is required.");

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
                return Result<Book>.Failure(ErrorType.ValidationError, "One or more author IDs are invalid.");
            }

            if (genres.Count != genreIds.Count)
            {
                return Result<Book>.Failure(ErrorType.ValidationError, "One or more genre IDs are invalid.");
            }



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
                book.Title = patchBookCommand.Title;
            if (patchBookCommand.Isbn != null)
                book.Isbn = patchBookCommand.Isbn;
            if (patchBookCommand.Description != null)
                book.Description = patchBookCommand.Description;
            if (patchBookCommand.PublishedDate.HasValue)
                book.PublishedDate = patchBookCommand.PublishedDate.Value;

            if (patchBookCommand.AuthorIds != null)
            {
                if(!patchBookCommand.AuthorIds.Any())
                {
                    return Result<Book>.Failure(ErrorType.ValidationError, "At least one author ID must be provided.");
                }

                var authorIds = patchBookCommand.AuthorIds.Distinct().ToList();

                var authors = await _context.Authors
                    .Where(a => authorIds.Contains(a.Id))
                    .ToListAsync();

                if (authors.Count != authorIds.Count)
                {
                    return Result<Book>.Failure(ErrorType.ValidationError, "One or more author IDs are invalid.");
                }

                book.Authors.Clear();
                foreach (var author in authors)
                {
                    book.Authors.Add(author);
                }
            }

            if (patchBookCommand.GenreIds != null)
            {
                if (!patchBookCommand.GenreIds.Any())
                {
                    return Result<Book>.Failure(ErrorType.ValidationError, "At least one genre ID must be provided.");
                }

                var genreIds = patchBookCommand.GenreIds.Distinct().ToList();

                var genres = await _context.Genres
                    .Where(g => genreIds.Contains(g.Id))
                    .ToListAsync();

                if (genres.Count != genreIds.Count)
                {
                    return Result<Book>.Failure(ErrorType.ValidationError, "One or more genre IDs are invalid.");
                }

                book.Genres.Clear();
                foreach (var genre in genres)
                {
                    book.Genres.Add(genre);
                }
            }

            await _context.SaveChangesAsync();
            return Result<Book>.Success(book);
        }
    }
    
}
