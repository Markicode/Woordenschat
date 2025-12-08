using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    internal class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }

        public int FamilyId { get; set; }
        public Family Family { get; set; } = null!;

        public int? UserId { get; set; }
        public User? User { get; set; } = null!;

        public Member? Member { get; set; }
        public Employee? Employee { get; set; }
    }
}
