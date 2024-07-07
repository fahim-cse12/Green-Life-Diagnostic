namespace Shared.DataTransferObject
{
    public record TicketDto : BaseDto
    {
        public Guid Id { get; init; }
        public Guid PatientId { get; init; }
        public Guid DoctorId { get; init; }
        public decimal Amount { get; init; }
        public decimal Discount { get; init; }
    }
}
