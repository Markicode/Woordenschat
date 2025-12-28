using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string? Isbn { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime? PublishedDate { get; set; }

        public List<BookCopy> BookCopies { get; set; } = new();
            
        public List<Author> Authors { get; set; } = new();
        public List<Genre> Genres { get; set; } = new();
        public List<Reservation> Reservations { get; set; } = new();
    }
}
