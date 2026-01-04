using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Dtos.Authors
{
    public class AuthorWithBooksDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? Bio { get; set; }
        public DateOnly? BirthDate { get; set; }
        public List<BookDto> Books { get; set; } = new();
    }
}
