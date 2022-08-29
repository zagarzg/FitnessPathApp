using FitnessPathApp.DomainLayer.Entities;
using FluentValidation;
using System;

namespace FitnessPathApp.BusinessLayer.Validators
{
    public class FoodLogValidator : AbstractValidator<FoodLog>
    {
        public FoodLogValidator()
        {
            RuleFor(log => log.Date)
                .NotEmpty()
                .WithMessage("Date must be set")
                .Must(date => IsNotInFuture(date))
                .WithMessage("Date cannot be set in future");

            RuleFor(log => log.UserId)
                .NotEmpty()
                .WithMessage("FoodLog must be connected to specific User");
        }

        public bool IsNotInFuture(DateTime date)
        {
            return date < DateTime.Now;
        }
    }
}
