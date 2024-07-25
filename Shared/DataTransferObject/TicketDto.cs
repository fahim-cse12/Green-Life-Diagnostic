namespace Shared.DataTransferObject
{
    public record TicketDto : BaseDto
    {
        public string Id { get; set; }
        public string PatientId { get; set; }
        public string DoctorId { get; set; }
        public decimal Amount { get; set; }
        public decimal Discount { get; set; }
    }
}
