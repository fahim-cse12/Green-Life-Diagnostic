using Entities.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Validators
{
    public class PatientInvestigationValidator : AbstractValidator<PatientInvestigation>
    {
        public PatientInvestigationValidator()
        {
            RuleFor(pi => pi.PayAmount).NotEmpty().WithMessage("Pay Amount is required.")
                                       .GreaterThan(0).WithMessage("Pay Amount must be non-zero.");
                                          
        }
    }
}
