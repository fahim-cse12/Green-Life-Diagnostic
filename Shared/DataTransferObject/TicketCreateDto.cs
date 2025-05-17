namespace Shared.DataTransferObject
{
    public record TicketCreateDto(Guid DoctorId, decimal Amount, decimal Discount);
    
}
