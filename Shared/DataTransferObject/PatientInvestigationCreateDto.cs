namespace Shared.DataTransferObject
{
    public record PatientInvestigationCreateDto(
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
    List<PatientInvestigationDetailCreateDto> PatientInvestigationDetailCreateDtos
);


}
