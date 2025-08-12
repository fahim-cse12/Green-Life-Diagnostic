using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObject
{
    public record PatientTicketDto
    {
        public string PatientUniqueId { get; set; }
        public string Name { get; init; }
        public string Gender { get; init; }
        public string Mobile { get; init; }
        public int Age { get; init; }
        public string Address { get; init; }
        public Guid DoctorId { get; set; }
        public string TicketUniqueId { get; set; }
        public int SerialNo { get; set; }
        public decimal Amount { get; set; }
        public decimal Discount { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
