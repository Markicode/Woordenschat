using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Books
{
    public class PatchBookDto
    {
        public string? Title { get; init; }
        public string? Isbn { get; init; }
        public string? Description { get; init; }
        public DateOnly? PublishedDate { get; init; }
        public List<int>? AuthorIds { get; init; }
        public List<int>? GenreIds { get; init; }
    }
}
