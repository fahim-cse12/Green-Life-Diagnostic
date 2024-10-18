using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObject
{
    public record PatientInvestigationDetailDto
    (
        Guid PatientInvestigationDetailId,
        Guid InvestigationId,
        string? ResultText,
        DateTime? ResultDate,
        decimal PaymentAmount,
        bool IsDelivered,
        string CreatedAt,
        string UpdatedAt
    );
   
}
