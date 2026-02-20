using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.Dtos.Books
{
    public class PatchBookDto : IValidatableObject
    {
        [MaxLength(200)]
        public string? Title { get; set; }

        //[MaxLength(13)]
        //public string? Isbn { get; set; }

        public JsonElement? Isbn { get; init; }

        //[MaxLength(2000)]
        //public string? Description { get; set; }

        public JsonElement? Description { get; init; }

        public JsonElement? PublishedDate { get; set; }

        [MinLength(1)]
        public List<int>? AuthorIds { get; set; }

        [MinLength(1)]
        public List<int>? GenreIds { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Title is null &&
                Isbn is null &&
                Description is null &&
                PublishedDate is null &&
                AuthorIds is null &&
                GenreIds is null)
            {
                yield return new ValidationResult(
                    "At least one field must be provided.",
                    new[] { nameof(PatchBookDto) });
            }
        }
    }
}
