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
        public Guid Id { get; init; }
        public Guid PatientId { get; init; }
        public Guid DoctorId { get; init; }
        public Guid InvestigationId { get; init; }
        public decimal DiscountAmount { get; init; }
        public decimal DueAmount { get; init; }
        public decimal PayAmount { get; init; }
        public DateTime DeliveryDate { get; init; }
        public bool IsDelivered { get; init; }
    }
}
