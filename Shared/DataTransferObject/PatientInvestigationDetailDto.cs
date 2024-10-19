using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObject
{
    public class PatientInvestigationDetailDto
    {
        public Guid PatientInvestigationDetailId { get; set; } // Corrected to match mapping
        public Guid InvestigationId { get; set; } // Mapped from InvestigationId
        public string InvestigationName { get; set; } // Mapped from Investigation
        public string Description { get; set; }      // Mapped from Investigation
        public string ResultText { get; set; }
        public decimal PaymentAmount { get; set; }
        public bool IsDelivered { get; set; }
        public bool IsActive { get; set; }
        public string CreatedAt { get; set; }        // Date formatted as string
        public string UpdatedAt { get; set; }        // Date formatted as string

        // Parameterless constructor (required by AutoMapper)
        public PatientInvestigationDetailDto() { }

        // Optional: Constructor with parameters (not used by AutoMapper, but can be helpful elsewhere)
        public PatientInvestigationDetailDto(
            Guid patientInvestigationDetailId,
            Guid investigationId,
            string investigationName,
            string description,
            string resultText,
            decimal paymentAmount,
            bool IsDelivered,
            bool isActive,
            string createdAt,
            string updatedAt)
        {
            PatientInvestigationDetailId = patientInvestigationDetailId;
            InvestigationId = investigationId;
            InvestigationName = investigationName;
            Description = description;
            ResultText = resultText;
            PaymentAmount = paymentAmount;
            IsDelivered = IsDelivered;
            IsActive = isActive;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
    }


}
