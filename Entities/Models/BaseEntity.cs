using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class BaseEntity
    {
        public bool Status { get; set; }
        [ForeignKey(nameof(User))]
        public string? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        [MaxLength(100)]
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
