using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObject
{
    public record FinancialRecordDto : BaseDto
    {
        public Guid Id { get; init; }
        public DateTime RecordDate { get; init; }
        public string UniqueId { get; init; }
        public string Purpose { get; init; }
        public decimal Income { get; init; }
        public decimal Expense { get; init; }
    }
}
