using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObject
{
    public record PatientInvestigationDetailCreateDto
    (
        Guid InvestigationId,
        string? ResultText,
        DateTime? ResultDate,
        decimal? PaidAmount,
        bool IsDelivered
    );
}
