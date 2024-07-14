using FluentValidation;
using Shared.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Validator
{
    public class TicketDtoValidator : AbstractValidator<TicketDto>
    {
        public TicketDtoValidator()
        {
            RuleFor(t => t.Amount).NotEmpty().WithMessage("Amount is required.").LessThanOrEqualTo(0).WithMessage("Amount should greater than 0"); ;

        }
    }
}
