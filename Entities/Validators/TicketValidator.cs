using Entities.Models;
using FluentValidation;

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
