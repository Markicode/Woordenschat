using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    internal class Author
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public string? bio { get; set; } 
        public DateTime? BirthDate { get; set; }

        public List<BookAuthor> BookAuthors { get; set; } = new();
    }
}
