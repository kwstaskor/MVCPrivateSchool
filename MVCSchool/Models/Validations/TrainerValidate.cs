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
        }
    }
}