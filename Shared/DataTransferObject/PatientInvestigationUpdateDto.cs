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
         bool  IsDelivered ,
    List<PatientInvestigationDetailUpdateDto> PatientInvestigationDetailUpdateDtos
    );
}
