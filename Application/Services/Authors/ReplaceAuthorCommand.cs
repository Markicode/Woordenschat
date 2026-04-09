using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Authors
{
    public sealed class ReplaceAuthorCommand
    {
        public int Id { get; init; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public DateOnly? BirthDate { get; init; }
        public string? Bio { get; init; }

        public ReplaceAuthorCommand(int id, string firstName, string lastName, DateOnly? birthDate, string? bio)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
            Bio = bio;
        }
    }
}
