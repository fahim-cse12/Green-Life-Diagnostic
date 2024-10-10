using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class PatientInvestigationDetail
    {
        public Guid PatientInvestigationDetailId { get; set; }

        [ForeignKey(nameof(PatientInvestigation))]
        public Guid PatientInvestigationId { get; set; } // Foreign key

        [ForeignKey(nameof(Investigation))]
        public Guid InvestigationId { get; set; } // Foreign key to Investigation table
        public string? ResultText { get; set; } // Result for each test
        public DateTime? ResultDate { get; set; } // Date when result was created
        public decimal PaidAmount { get; set; }
        public bool IsDelivered { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
