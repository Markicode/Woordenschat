using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

namespace Domain.Entities
{
    internal class Reservation
    {
        public int Id { get; set; }

        public int MemberId { get; set; }
        public Member Member { get; set; } = null!;

        public int BookId { get; set; }
        public Book Book { get; set; } = null!;

        public DateTime ReservationDate { get; set; }
        public DateTime? ExpirationDate { get; set; }

        public ReservationStatus Status { get; set; } = ReservationStatus.Pending;
        public int queuePosition { get; set; }
    }
}
