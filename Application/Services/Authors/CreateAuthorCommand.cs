using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Authors
{
    public sealed class CreateAuthorCommand
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public DateOnly? BirthDate { get; init; }
        public string? Bio { get; init; }

        public CreateAuthorCommand(string firstName, string lastName, DateOnly? birthDate, string? bio)
        {
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
            Bio = bio;
        }
    }
}
