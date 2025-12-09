using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    internal class Book
    {
        public int Id { get; set; }
        public string isbn { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime? PublishedDate { get; set; }

        public List<BookAuthor> BookAuthors { get; set; } = new();
        public List<BookCopy> BookCopies { get; set; } = new();

        public List<Genre> Genres { get; set; } = new();
    }
}
