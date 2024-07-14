using Entities.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Validators
{
    public class PatientValidator : AbstractValidator<Patient>
    {
        public PatientValidator() 
        {
            RuleFor(p => p.Age).NotEmpty().WithMessage("Age is required.");
            RuleFor(p => p.Gender).NotEmpty().WithMessage("Gender is required.");
            RuleFor(p => p.Mobile).NotEmpty().WithMessage("Mobile is required.");

            RuleFor(p => p).Must(p => p.Mobile.Length == 11).WithMessage("Mobile No should 11 digit");
            RuleFor(p => p.Address).NotEmpty().WithMessage("Address is required.");
        }   
    }
}
