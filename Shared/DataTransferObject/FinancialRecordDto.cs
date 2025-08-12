using static Shared.Utility.EnumValue;

namespace Shared.DataTransferObject
{
    public record FinancialRecordDto : BaseDto
    {
        public Guid Id { get; init; }
        public DateTime RecordDate { get; init; }
        public string UniqueId { get; init; }
        public required string Purpose { get; init; }
        public FinancialType FinancialType { get; set; }
        public decimal Income { get; init; }
        public decimal Expense { get; init; }
        public decimal Asset { get; init; }
        public decimal Liability { get; init; }
        public string? AttachmentFileUrl { get; init; }
    }
}
