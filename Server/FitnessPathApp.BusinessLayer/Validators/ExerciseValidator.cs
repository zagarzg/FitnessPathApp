using FitnessPathApp.DomainLayer.Entities;
using FluentValidation;

namespace FitnessPathApp.BusinessLayer.Validators
{
    public class ExerciseValidator : AbstractValidator<Exercise>
    {
        public ExerciseValidator()
        {
            RuleFor(e => e.Weight)
                .NotEmpty()
                .WithMessage("Exercise weight must be set")
                .GreaterThan(0)
                .WithMessage("Exercise weight must be greater then 0");

            RuleFor(e => e.Sets)
                .NotEmpty()
                .WithMessage("Exercise sets must be set")
                .GreaterThan(0)
                .WithMessage("Exercise sets must be greater then 0");

            RuleFor(e => e.Reps)
                .NotEmpty()
                .WithMessage("Exercise reps must be set")
                .GreaterThan(0)
                .WithMessage("Exercise reps must be greater then 0");

            RuleFor(e => e.TrainingLogId)
                .NotEmpty()
                .WithMessage("Exercise must be connected to specific TrainingLog");
        }
    }
}
