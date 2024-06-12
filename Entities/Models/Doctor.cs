using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Doctor : BaseEntity
    {
        public string Name { get; set; }
        public decimal Fee { get; set; }
        public string Specialty { get; set; }
        public string ScheduledDay { get; set; }
    }
}
