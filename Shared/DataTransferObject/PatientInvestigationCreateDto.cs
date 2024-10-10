namespace Shared.DataTransferObject
{
    public record PatientInvestigationCreateDto(
        string? PatientUniqueId,
        Guid? DoctorId,
        Guid InvestigationId,
        string? InvestigationUniqueId, 
        string PatientName,
        int PatientAge,
        string PatientMobileNo,
        string PatientAddress,
        decimal? PaidAmount,
        decimal? DueAmount,
        decimal? DiscountAmount, 
        DateTime? DeliveryDate, 
        bool IsDelivered
    );
    
}
