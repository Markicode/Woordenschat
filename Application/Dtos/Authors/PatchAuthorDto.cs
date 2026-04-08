using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.Dtos.Authors
{
    public class PatchAuthorDto : IValidatableObject
    {
        [MaxLength(100)]
        public string? FirstName { get; set; }

        [MaxLength(100)] 
        public string? LastName { get; set; }

        [MaxLength(2000)]
        public JsonElement? Bio {  get; set; }

        public JsonElement? BirthDate { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (FirstName is null &&
                LastName is null &&
                Bio is null &&
                BirthDate is null)
            {
                yield return new ValidationResult(
                    "At least one field must be provided.");
            }
        }
    }
}
