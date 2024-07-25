using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObject
{
    public record DoctorDto : BaseDto
    {
        public string Id { get; init; }
        public string Name { get; init; }
        public decimal Fee { get; init; }
        public string Speciality { get; init; }
        public string ScheduledDay { get; init; }
        public string ContactNo { get; init; }
    }
}
