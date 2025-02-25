using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartClinic.Domain.Entities
{
    public class Payment
    {
        public Guid Id { get; set; }
        public Guid AppointmentId { get; set; }
        public decimal Amount { get; set; }
        public string? eSewaTransactionId { get; set; }
        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
        public Appointment Appointment { get; set; } = null!;
    }
}
