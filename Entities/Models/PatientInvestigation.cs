using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class PatientInvestigation : BaseEntity
    {
        public Guid PatientInvestigationId { get; set; }
        public string? PatientUniqueId { get; set; }
        public Guid? DoctorId { get; set; }
        public string PatientInvestigationUniqueId { get; set; }
        public string PatientName { get; set; }
        public int PatientAge { get; set; }
        public string PatientMobileNo { get; set; }
        public string PatientAddress { get; set; }

        // Financials
        public decimal TotalAmount { get; private set; }
        public decimal DiscountAmount { get; private set; }
        public decimal PaidAmount { get; set; }
        public decimal DueAmount { get; private set; }

        public DateTime? DeliveryDate { get; set; }
        public bool IsDelivered { get; set; }

        public ICollection<PatientInvestigationDetail> InvestigationDetails { get; set; } = new List<PatientInvestigationDetail>();

        // Method to update financial data
        public void CalculateFinancials(decimal discountPercentage)
        {
            // Calculate the total from all details
            TotalAmount = InvestigationDetails.Sum(x => x.PaymentAmount);

            // Apply discount on the total amount
            DiscountAmount = TotalAmount * (discountPercentage / 100);

            // PaidAmount is not recalculated; it's set externally, maybe initially zero or a partial payment
            PaidAmount = PaidAmount >= 0 ? PaidAmount : 0; // Ensure that PaidAmount is non-negative.

            // Now update DueAmount
            UpdateDueAmount();
        }

        // Method to update due amount
        public void UpdateDueAmount()
        {
            DueAmount = TotalAmount - PaidAmount - DiscountAmount;
        }

        // Method to add a detail and recalculate
        public void AddInvestigationDetail(PatientInvestigationDetail detail)
        {
            InvestigationDetails.Add(detail);
            CalculateFinancials(DiscountAmount); // Update financials after adding
        }
    }

    
}
