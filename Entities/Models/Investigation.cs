using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Investigation : BaseEntity
    {
        [Column("InvestigationId")]
        public Guid Id { get; set; }
        public string InvestigationName { get; set; }
        public decimal Cost { get; set; }
        public string Description { get; set; }
    }
}
