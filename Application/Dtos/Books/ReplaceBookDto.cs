using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class ReplaceBookDto
    {
        public int BookId { get; }
        public string Isbn { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateOnly PublishedDate { get; set; }
        public List<int> AuthorIds { get; set; } = new();
        public List<int> GenreIds { get; set; } = new();
    }
}
