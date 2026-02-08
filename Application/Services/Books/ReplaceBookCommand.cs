using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Books
{
    public sealed class ReplaceBookCommand
    {
        public int BookId { get; }
        public string? Isbn { get; }
        public string Title { get; }
        public string? Description { get; }
        public DateOnly? PublishedDate { get; }
        public List<int> AuthorIds { get; }
        public List<int> GenreIds { get; } 

        public ReplaceBookCommand(int bookId, string? isbn, string title, string? description, DateOnly? publishedDate, List<int> authorIds, List<int> genreIds)
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
