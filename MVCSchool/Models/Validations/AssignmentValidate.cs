using System;
using FluentValidation;

namespace MVCSchool.Models.Validations
{
    public class AssignmentValidate : AbstractValidator<Assignment>
    {
        public AssignmentValidate()
        {
            RuleFor(a => a.Title).NotEmpty().WithMessage("Required")
                .Length(2, 50).WithMessage("Min Length(2) , Max Length(50)")
                .Matches("^[a-zA-Z_ ]*$").WithMessage("Only Letters");
           
            RuleFor(a => a.Description).NotEmpty().WithMessage("Required")
                .Length(2, 50).WithMessage("Min Length(2) , Max Length(50)")
                .Matches("^[a-zA-Z_ ]*$").WithMessage("Only Letters");

            RuleFor(c => c.Submission).NotEmpty().WithMessage("Required");

            RuleFor(a => a.OralMark).NotEmpty().WithMessage("Required")
                .InclusiveBetween(0, 100).WithMessage("Value must be number between 1, 100");

            RuleFor(a => a.TotalMark).NotEmpty().WithMessage("Required")
                .InclusiveBetween(0, 100).WithMessage("Value must be number between 1, 100");
        }
    }
}