using FitnessPathApp.DomainLayer.Entities;
using FluentValidation;

namespace FitnessPathApp.BusinessLayer.Validators
{
    public class ExerciseChoiceValidator : AbstractValidator<ExerciseChoice>
    {
        public ExerciseChoiceValidator()
        {
            RuleFor(e => e.Name)
                .NotEmpty()
                .WithMessage("Exercise name must be set");

            RuleFor(e => e.ExerciseType)
                .NotEmpty()
                .WithMessage("Exercise type must be set");

        }
    }
}
