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
        [MaxLength(100)]
        public string InvestigationName { get; set; }
        public decimal Cost { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
    }
}
