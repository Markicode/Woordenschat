using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    internal class Member
    {
        public int Id { get; set; }

        public int PersonId { get; set; }
        public Person Person { get; set; } = null!;

        public DateTime MembershipStarted { get; set; }
        public bool IsActive { get; set; } = true;

        public List<Loan> Loans { get; set; } = new();
    }
}
