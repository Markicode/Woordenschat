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
        public int Id { get; private set; }
        public string FirstName { get; private set; } = string.Empty;
        public string LastName { get; private set; } = string.Empty;
        public DateOnly DateOfBirth { get; private set; }

        public int FamilyId { get; private set; }
        public Family Family { get; private set; } = null!;

        public int? UserId { get; private set; }
        public User? User { get; private set; } = null!;

        public Member? Member { get; private set; }
        public Employee? Employee { get; private set; }

        public PersonStatus Status { get; private set; } = PersonStatus.Active;
        public DateTime? StatusChangedAtUtc { get; private set; }

        private Person() { }

        public Person(string firstName, string lastName, DateOnly dateOfBirth, int familyId)
        {
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            FamilyId = familyId;
            
            Status = PersonStatus.Active;
            StatusChangedAtUtc = DateTime.UtcNow;
        }


        public void Activate()
        {
            if (Status != PersonStatus.Deactivated)
                throw new InvalidOperationException("Only deactivated persons can be activated.");
            Status = PersonStatus.Active;
            StatusChangedAtUtc = DateTime.UtcNow;
        }

        public void Deactivate()
        {
            if (Status != PersonStatus.Active)
                throw new InvalidOperationException("Only active persons can be deactivated.");

            Status = PersonStatus.Deactivated;
            StatusChangedAtUtc = DateTime.UtcNow;
        }

        public void Anonymize()
        {
            if (Status == PersonStatus.Anonymized)
                throw new InvalidOperationException("Person already anonymized.");

            FirstName = "Deleted";
            LastName = "User";

            Status = PersonStatus.Anonymized;
            StatusChangedAtUtc = DateTime.UtcNow;
            DateOfBirth = new DateOnly(1900, 1, 1);
        }

        public void UpdatePersonalInfo(string firstName, string lastName, DateOnly dateOfBirth)
        {
            if (Status == PersonStatus.Anonymized)
                throw new InvalidOperationException("Cannot modify anonymized person.");

            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
        }

        public void ChangeFamily(Family family)
        {
            if(family  == null) throw new ArgumentNullException(nameof(family));
            if (Status == PersonStatus.Anonymized)
                throw new InvalidOperationException("Can't change family of anonymized user.");

            Family = family;
            FamilyId = family.Id;
        }
    }

    
}
