using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObject
{
    public record InvestigationDto : BaseDto
    {
        public Guid Id { get; init; }
        public string InvestigationName { get; init; }
        public decimal Cost { get; init; }
        public string Description { get; init; }
    }
}
