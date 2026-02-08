
using System.ComponentModel.DataAnnotations;

namespace Application.Dtos
{
    public class CreateBookDto
    {
        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(13)]
        public string? Isbn { get; set; }

        [MaxLength(2000)]
        public string? Description { get; set; }

        public DateOnly? PublishedDate { get; set; }

        [MinLength(1)]
        public List<int> AuthorIds { get; set; } = new();

        [MinLength(1)]
        public List<int> GenreIds { get; set; } = new();

    }
}

