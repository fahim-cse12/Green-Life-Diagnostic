using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class FinancialRecord : BaseEntity
    {
        [Column("FinancialId")]
        public Guid Id { get; set; }

        public DateTime RecordDate { get; set; }
        public string UniqueId { get; set; }
        public string Purpose { get; set; }
        public decimal Income { get; set; }
        public decimal Expense { get; set; }
    }
}
