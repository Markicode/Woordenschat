using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

namespace Domain.Entities
{
    public class Member
    {
        public int Id { get; set; }

        public int PersonId { get; set; }
        public string MembershipNumber { get; set; } = string.Empty;

        public Person Person { get; set; } = null!;

        public DateTime MembershipStarted { get; set; }
        public bool IsActive { get; set; } = true;

        public List<Reservation> Reservations { get; set; } = new();
        public List<Loan> Loans { get; set; } = new();

        public MemberStatus Status { get; private set; }
        public DateTime StatusChangedAtUtc { get; private set; }
    }
}
