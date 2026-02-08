using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class ReplaceBookDto
    {
        [MaxLength(13)]
        public string Isbn { get; set; } = string.Empty;

        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(2000)]
        public string? Description { get; set; } = string.Empty;

        public DateOnly? PublishedDate { get; set; }

        [MinLength(1)]
        public List<int> AuthorIds { get; set; } = new();

        [MinLength(1)]
        public List<int> GenreIds { get; set; } = new();
    }
}
