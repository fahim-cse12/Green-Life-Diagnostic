using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObject
{
    public record PatientInvestigationUpdateDto
    (
         Guid PatientInvestigationId,
         string? PatientUniqueId,
         Guid? DoctorId,
         string? InvestigationUniqueId,
         string PatientName,
         int PatientAge,
         string PatientMobileNo,
         string PatientAddress,
         decimal? PaidAmount,
         decimal? DiscountAmount,
         string? DeliveryDate,
         List<PatientInvestigationDetailUpdateDto> PatientInvestigationDetailUpdateDtos
    );
}
