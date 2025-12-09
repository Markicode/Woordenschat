using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class BookCopy
    {
        public int Id { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; } = null!;

        public string InventoryNumber { get; set; } = string.Empty;
        public string? Condition { get; set; }

        public bool IsAvailable { get; set; }
        public List<Loan> Loans { get; set; } = new();
    }
}
