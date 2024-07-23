namespace Shared.DataTransferObject
{
    public record TicketDto : BaseDto
    {
        public Guid Id { get; set; }
        public Guid PatientId { get; set; }
        public Guid DoctorId { get; set; }
        public decimal Amount { get; set; }
        public decimal Discount { get; set; }
    }
}
