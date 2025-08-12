namespace Shared.DataTransferObject
{
    public record TicketDto : BaseDto
    {
        public Guid Id { get; set; }
        public Guid PatientId { get; set; }
        public Guid DoctorId { get; set; }
        public required string UniqueId { get; set; }
        public required int SerialNo { get; set; } = 0;
        public required decimal Amount { get; set; }
        public decimal Discount { get; set; }
    }
}
