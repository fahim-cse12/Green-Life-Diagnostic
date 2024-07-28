namespace Shared.DataTransferObject
{
    public record PatientInvestigationCreateDto(Guid PatientId, Guid DoctorId, Guid InvestigationId, decimal PayAmount, decimal DueAmount, decimal DiscountAmount, DateTime DeliveryDate, bool IsDelivered);
    
}
