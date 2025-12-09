using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Family
    {
        public int Id { get; set; }
        public string FamilyName { get; set; } = string.Empty;
        public List<Person> Members { get; set; } = new();
    }
}
