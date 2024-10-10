namespace Shared.DataTransferObject
{
    public record PatientInvestigationDto : BaseDto
    {
        public Guid Id { get; init; }
        public string? PatientUniqueId { get; init; }
        public Guid? DoctorId { get; init; }
        public Guid InvestigationId { get; init; }
        public string InvestigationUniqueId { get; init; }
        public string PatientName { get; set; }
        public int PatientAge { get; set; }
        public string PatientMobileNo { get; set; }
        public string PatientAddress { get; set; }
        public decimal DiscountAmount { get; init; }
        public decimal DueAmount { get; init; }
        public decimal PaidAmount { get; set; }
        public DateTime DeliveryDate { get; init; }
        public bool IsDelivered { get; init; }
    }
}
