using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObject
{
    public record PatientInvestigationDto : BaseDto
    {
        public string Id { get; init; }
        public string PatientId { get; init; }
        public string DoctorId { get; init; }
        public string InvestigationId { get; init; }
        public decimal DiscountAmount { get; init; }
        public decimal DueAmount { get; init; }
        public decimal PayAmount { get; init; }
        public DateTime DeliveryDate { get; init; }
        public bool IsDelivered { get; init; }
    }
}
