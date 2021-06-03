using System;
using FluentValidation;

namespace MVCSchool.Models.Validations
{
    public class CourseValidate : AbstractValidator<Course>
    {
        public CourseValidate()
        {
            RuleFor(c => c.Title).NotEmpty().WithMessage("Required")
                .Length(2, 50).WithMessage("Min Length(2) , Max Length(50)")
                .Matches("^[a-zA-Z_ ]*$").WithMessage("Only Letters");

            RuleFor(c => c.Stream).NotEmpty().WithMessage("Required")
                .Length(2, 50).WithMessage("Min Length(2) , Max Length(50)")
                .Matches("^[a-zA-Z_ ]*$").WithMessage("Only Letters");

            RuleFor(c => c.Type).NotEmpty().WithMessage("Required")
                .Length(2, 50).WithMessage("Min Length(2) , Max Length(50)")
                .Matches("^^[a-zA-Z_ ]*$").WithMessage("Only Letters");

            RuleFor(c => c.StartDate).NotEmpty().WithMessage("Required");

            RuleFor(c => c.EndDate).NotEmpty().WithMessage("Required");
        }
    }
}