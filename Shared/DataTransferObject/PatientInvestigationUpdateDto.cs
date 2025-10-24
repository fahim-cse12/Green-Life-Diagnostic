namespace Shared.DataTransferObject
{
    public class PatientInvestigationUpdateDto
    {
        public Guid PatientInvestigationId { get; set; }
        public string? PatientUniqueId { get; set; }
        public Guid? DoctorId { get; set; }
        public string? InvestigationUniqueId { get; set; }
        public string PatientName { get; set; } = string.Empty;
        public int PatientAge { get; set; }
        public string PatientMobileNo { get; set; } = string.Empty;
        public string PatientAddress { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public decimal? PaidAmount { get; set; }
        public decimal? DueAmount { get; set; }
        public decimal? DiscountAmount { get; set; }
        public string? DeliveryDate { get; set; }
        public bool IsDelivered { get; set; }

        public List<PatientInvestigationDetailUpdateDto> PatientInvestigationDetailUpdateDtos { get; set; }
            = new List<PatientInvestigationDetailUpdateDto>();

        
    }



}
