using System.ComponentModel.DataAnnotations.Schema;

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
