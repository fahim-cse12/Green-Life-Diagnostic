using Entities.Models;
using FluentValidation;

namespace Entities.Validators
{
    public class PatientInvestigationDetailValidator : AbstractValidator<PatientInvestigationDetail>
    {
        public PatientInvestigationDetailValidator()
        {
            RuleFor(id => id.InvestigationId).NotNull().WithMessage("Investigation Id is required.");
            RuleFor(id => id.PatientInvestigationId).NotNull().WithMessage("PatientInvestigation Id is required.");
        }
    }
}
