using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Books
{
    public sealed class PatchBookCommand
    {
        public int BookId { get; }
        public string? Title { get; }
        public string? Description { get; }
        public string? Isbn { get; }
        public DateOnly? PublishedDate { get; }
        public IReadOnlyCollection<int>? AuthorIds { get; }
        public IReadOnlyCollection<int>? GenreIds { get; }

        public PatchBookCommand(
            int bookId,
            string? isbn,
            string? title,
            string? description,
            DateOnly? publishedDate,
            IReadOnlyCollection<int>? authorIds,
            IReadOnlyCollection<int>? genreIds)
        {
            BookId = bookId;
            Isbn = isbn;
            Title = title;
            Description = description;
            PublishedDate = publishedDate;
            AuthorIds = authorIds;
            GenreIds = genreIds;
        }
    }
}
