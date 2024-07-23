using Entities.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Validators
{
    public class UserValidator :  AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(u => u.FirstName).NotEmpty().WithMessage("User First Name is required.");
            //RuleFor(u => u.LoginId).NotEmpty().WithMessage("User Login Id is required.");
            //RuleFor(u => u.Email).NotEmpty().EmailAddress().WithMessage("User Valid Email is required.");

        }  
    }
}
