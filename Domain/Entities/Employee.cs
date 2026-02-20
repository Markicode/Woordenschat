using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

namespace Domain.Entities
{
    public class Employee
    {
        public int Id { get; set; }

        public int PersonId { get; set; }
        public Person Person { get; set; } = null!;

        public DateOnly EmploymentDate { get; set; }
        public string Position { get; set; } = string.Empty;

        public List<NewsPost> CreatedNewsPosts { get; set; } = new();

        public EmployeeStatus Status { get; private set; }
        public DateTime? StatusChangedAtUtc { get; private set; }
    }
}
