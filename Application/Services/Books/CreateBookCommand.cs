using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Books
{
    public class CreateBookCommand
    {
        public string? Isbn { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime? PublishedDate { get; set; }
        public List<int> AuthorIds { get; set; } = new();
        public List<int> GenreIds { get; set; } = new();

        public CreateBookCommand(string? isbn, string title, string? description, DateTime? publishedDate, List<int> authorIds, List<int> genreIds)
        {
            Isbn = isbn;
            Title = title;
            Description = description;
            PublishedDate = publishedDate;
            AuthorIds = authorIds;
            GenreIds = genreIds;
        }
    }
}
