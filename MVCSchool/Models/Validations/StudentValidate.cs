using System;
using FluentValidation;

namespace MVCSchool.Models.Validations
{
    public class StudentValidate : AbstractValidator<Student>
    {
        public StudentValidate()
        {
            RuleFor(s => s.FirstName).NotEmpty().WithMessage("Required")
                .Length(2, 50).WithMessage("Min Length(2) , Max Length(50)")
                .Matches("^[a-zA-Z_ ]*$").WithMessage("Only Letters");

            RuleFor(s => s.LastName).NotEmpty().WithMessage("Required")
                .Length(2, 50).WithMessage("Min Length(2) , Max Length(50)")
                .Matches("^[a-zA-Z_ ]*$").WithMessage("Only Letters");

            RuleFor(s => s.DateOfBirth).NotEmpty().WithMessage("Required");

            RuleFor(s => s.TuitionFee).NotEmpty()
                .WithMessage("Required")
                .InclusiveBetween(0, 10000).WithMessage("Value must be number between 1, 10000");
        }

    }
}