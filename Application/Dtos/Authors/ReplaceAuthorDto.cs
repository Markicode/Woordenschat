using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Dtos.Authors
{
    public class ReplaceAuthorDto
    {
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; } = string.Empty;

        public DateOnly? BirthDate { get; set; }

        [MaxLength(2000)]
        public string? Bio { get; set; }

        public List<int> Books { get; set; } = new();
    }
}
