using Entities.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
