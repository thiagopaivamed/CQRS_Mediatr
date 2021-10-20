using CQRS_Mediatr.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS_Mediatr.Domain.Validation
{
    public class CustomerValidation : AbstractValidator<Customer>
    {
        public CustomerValidation()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull().WithMessage("{PropertyName} is required")
                .MinimumLength(6).WithMessage("Use more characters")
                .MaximumLength(30).WithMessage("Use less characters");

            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull().WithMessage("{PropertyName} is required")                
                .MaximumLength(50).WithMessage("Use less characters")
                .EmailAddress().WithMessage("Invalid email"); 
        }
    }
}
