using FluentValidation;

namespace MVCSchool.Models.Validations
{
    public class TrainerValidate : AbstractValidator<Trainer>
    {
        public TrainerValidate()
        {
            RuleFor(t => t.FirstName).NotEmpty().WithMessage("Required")
                .Length(2, 50).WithMessage("Min Length(2) , Max Length(50)")
                .Matches("^[a-zA-Z_ ]*$").WithMessage("Only Letters");

            RuleFor(t => t.LastName).NotEmpty().WithMessage("Required")
                .Length(2, 50).WithMessage("Min Length(2) , Max Length(50)")
                .Matches("^[a-zA-Z_ ]*$").WithMessage("Only Letters");

            RuleFor(t => t.Subject).NotEmpty().WithMessage("Required")
                .Length(2, 50).WithMessage("Min Length(2) , Max Length(50)");

            RuleFor(t => t.YearsOfExperience).NotEmpty().WithMessage("Required")
                .Length(1, 2).WithMessage("Not Valid")
                .Matches("^[0-9]*$").WithMessage("Only numbers");

            RuleFor(t => t.Bio).NotEmpty().WithMessage("Required")
                .MinimumLength(20).WithMessage("At Least 20 characters")
                .MaximumLength(1000).WithMessage("Max Length(1000)");
        }
    }
}