using Entities.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Validators
{
    public class TicketValidator : AbstractValidator<Ticket>
    {
        public TicketValidator() 
        {
            RuleFor(t => t.Amount).NotEmpty().WithMessage("Ticket Amount is required.")
                                  .GreaterThan(0).WithMessage("Ticket Amount should not be 0");

        }    
    }
}
