using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObject
{
    public record PatientDto : BaseDto
    {
        public string Id { get; init; }
        public string Name { get; init; }
        public string Gender { get; init; }
        public string Mobile { get; init; }
        public int Age { get; init; }
        public string Address { get; init; }
        public int PatientType { get; init; } // "Old" or "New"
    }
}
