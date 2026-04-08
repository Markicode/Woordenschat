using Application.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Authors
{
    public sealed class PatchAuthorCommand
    {
        public int AuthorId { get; }
        public string? FirstName { get; }
        public string? LastName { get; }
        public Optional<string?> Bio { get; }
        public Optional<DateOnly?> BirthDate { get; }


        public PatchAuthorCommand(int authorId, string? firstName, string? lastName, Optional<string?> bio, Optional<DateOnly?> birthDate)
        {
            AuthorId = authorId;
            FirstName = firstName;
            LastName = lastName;
            Bio = bio;
            BirthDate = birthDate;
        }
    }
}
