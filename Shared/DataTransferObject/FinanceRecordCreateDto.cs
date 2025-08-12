using static Shared.Utility.EnumValue;

namespace Shared.DataTransferObject
{
    public record FinanceRecordCreateDto(DateTime RecordDate,
        string Purpose, 
        FinancialType FinancialType, 
        decimal Amount,
        string? AttachmentFileUrl);
    
}
