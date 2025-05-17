namespace Shared.DataTransferObject
{
    public record FinanceRecordCreateDto(DateTime RecordDate, string Purpose, decimal Income,decimal Expense);
    
}
