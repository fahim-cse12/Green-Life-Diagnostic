using Entities.Models;
using FluentValidation;

namespace Entities.Validators
{
    public class TicketValidator : AbstractValidator<Ticket>
    {
        public TicketValidator()
        {
            RuleFor(t => t.Amount).NotEmpty().WithMessage("Amount is required.").LessThanOrEqualTo(0).WithMessage("Amount should greater than 0"); ;
                   
        }
    }
}
