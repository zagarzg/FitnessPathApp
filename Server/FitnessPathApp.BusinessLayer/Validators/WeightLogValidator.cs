using FitnessPathApp.DomainLayer.Entities;
using FluentValidation;
using System;

namespace FitnessPathApp.BusinessLayer.Validators
{
    public class WeightLogValidator : AbstractValidator<WeightLog>
    {
        public WeightLogValidator()
        {
            RuleFor(log => log.Value)
                .NotEmpty()
                .WithMessage("Weight must be set")
                .GreaterThan(0)
                .WithMessage("Weight must be greater than 0");

            RuleFor(log => log.Date)
                .NotEmpty()
                .WithMessage("Date must be set")
                .Must(date => IsNotInFuture(date))
                .WithMessage("Date cannot be set in future");

            RuleFor(log => log.UserId)
                .NotEmpty()
                .WithMessage("WeightLog must be connected to specific User");
        }

        public bool IsNotInFuture(DateTime date)
        {
            return date < DateTime.Now;
        }
    }
}
