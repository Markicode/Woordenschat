using Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Books
{
    public class BookService : IBookService
    {
        public async Task<CreateBookResult> CreateBookAsync(CreateBookCommand command)
        {
            var genreIds = command.GenreIds.Distinct().ToList();

            /** TODO: refactor
            var genres = await _context.Genres
    .Where(g => genreIds.Contains(g.Id))
    .ToListAsync();

            if (genres.Count != genreIds.Count)
            {
                return BadRequest("One or more genre IDs are invalid.");
            }

            var book = new Book
            {
                Title = dto.Title,
                Description = dto.Description,
                Isbn = dto.Isbn,
                PublishedDate = dto.PublishedDate,
                Genres = genres
            };
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            var bookDto = new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                Isbn = book.Isbn,
                PublishedDate = book.PublishedDate,
                Genres = genres
                    .OrderBy(g => g.Name)
                .Select(g => new GenreDto
                {
                    Id = g.Id,
                    Name = g.Name
                })
                .ToList()
            };
            return CreatedAtAction(nameof(GetById), new { id = book.Id }, bookDto); **/
        }
    }
    }
}
