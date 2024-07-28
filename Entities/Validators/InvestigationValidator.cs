using Entities.Models;
using FluentValidation;

namespace Entities.Validators
{
    public class InvestigationValidator : AbstractValidator<Investigation>
    {
        public InvestigationValidator()
        {
            RuleFor(I => I.InvestigationName).NotEmpty().WithMessage("Investigation Name is required.")
                                         .Length(1, 100).WithMessage("Investigation must be between 1 and 100 characters.");

            RuleFor(I => I.Cost).NotEmpty().WithMessage("Cost is required.").GreaterThan(0).WithMessage("Cost should greater than 0");
            RuleFor(I => I.Description).NotEmpty().WithMessage("Description is required.")
                                       .Length(1, 250).WithMessage("Description must be between 1 and 250 characters.");
        }
    }
}
