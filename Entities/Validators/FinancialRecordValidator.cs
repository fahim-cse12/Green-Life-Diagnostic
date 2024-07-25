using Entities.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Validators
{
    public class FinancialRecordValidator : AbstractValidator<FinancialRecord>
    {
        public FinancialRecordValidator()
        {
            RuleFor(record => record.Purpose).NotEmpty().WithMessage("Purpose is required.")
                                             .Length(1, 250).WithMessage("Purpose must be between 1 and 250 characters.");

            RuleFor(record => record).Must(record => record.Income != 0 || record.Expense != 0)
                                     .WithMessage("Either Income or Expense must be non-zero.");
        }
    }
}
