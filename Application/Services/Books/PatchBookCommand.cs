using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common;

namespace Application.Services.Books
{
    public sealed class PatchBookCommand
    {
        public int BookId { get; }
        public string? Title { get; }
        public Optional<string?> Description { get; }
        public Optional<string?> Isbn { get; }
        public Optional<DateOnly?> PublishedDate { get; }
        public IReadOnlyCollection<int>? AuthorIds { get; }
        public IReadOnlyCollection<int>? GenreIds { get; }

        public PatchBookCommand(
            int bookId,
            Optional<string?> isbn,
            string? title,
            Optional<string?> description,
            Optional<DateOnly?> publishedDate,
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
