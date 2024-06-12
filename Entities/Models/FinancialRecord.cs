using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class FinancialRecord : BaseEntity
    {
        public DateTime Date { get; set; }
        public string Purpose { get; set; }
        public decimal Income { get; set; }
        public decimal Expense { get; set; }
    }
}
