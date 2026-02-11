using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

namespace Domain.Entities
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateOnly DateOfBirth { get; set; }

        public int FamilyId { get; set; }
        public Family Family { get; set; } = null!;

        public int? UserId { get; set; }
        public User? User { get; set; } = null!;

        public Member? Member { get; set; }
        public Employee? Employee { get; set; }

        public PersonStatus Status { get; private set; }
        public DateTime? StatusChangedAtUtc { get; private set; }
    }

    
}
