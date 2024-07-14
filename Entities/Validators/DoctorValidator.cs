using Entities.Models;
using FluentValidation;

namespace Entities.Validators
{
    public class DoctorValidator : AbstractValidator<Doctor>
    {
        public DoctorValidator()
        {
            RuleFor(d => d.Name).NotEmpty().WithMessage("Name is required.")
            .MaximumLength(50).WithMessage("Name must be less than 50 characters.");

            RuleFor(d => d.Fee).NotEmpty().WithMessage("Fee is required.").GreaterThan(0).WithMessage("Fee should greater than 0");

            RuleFor(d => d.Speciality).NotEmpty().WithMessage("Speciality is required.")
               .MaximumLength(50).WithMessage("Speciality must be less than 50 characters.");

        }
    }
}
