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
            RuleFor(pi => pi.PatientName).NotNull().WithMessage("Patient Name is required.");
            RuleFor(pi => pi.PatientAddress).NotNull().WithMessage("Patient Address is required.");
            RuleFor(pi => pi.PatientAge).NotNull().WithMessage("Patient Age is required.");
            RuleFor(pi => pi.PatientMobileNo).NotNull().WithMessage("Patient MobileNo is required.");
            //RuleFor(pi => pi.PaidAmount).NotEmpty().WithMessage("Paid Amount is required.").GreaterThan(0).WithMessage("Pay Amount must be non-zero.");
                                          
        }
    }
}
