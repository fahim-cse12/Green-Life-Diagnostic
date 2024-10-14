namespace Shared.DataTransferObject
{
    public record PatientInvestigationDto : BaseDto
    {
        public Guid PatientInvestigationId { get; set; }
        public string? PatientUniqueId { get; set; }
        public Guid? DoctorId { get; set; }
        public string PatientInvestigationUniqueId { get; set; }
        public string PatientName { get; set; }
        public int PatientAge { get; set; }
        public string PatientMobileNo { get; set; }
        public string PatientAddress { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal DueAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public bool IsDelivered { get; set; }
        public List<PatientInvestigationDetailDto> InvestigationDetails { get; set; }
    }
}
