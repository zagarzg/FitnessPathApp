using FitnessPathApp.DomainLayer.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessPathApp.BusinessLayer.Validators
{
    public class TrainingLogValidator : AbstractValidator<TrainingLog>
    {
        public TrainingLogValidator()
        {
            RuleFor(log => log.Date)
                .NotEmpty()
                .WithMessage("Date must be set")
                .Must(date => IsNotInFuture(date))
                .WithMessage("Date cannot be set in future");

            RuleFor(log => log.UserId)
                .NotEmpty()
                .WithMessage("TrainingLog must be connected to specific User");
        }

        public bool IsNotInFuture(DateTime date)
        {
            return date < DateTime.Now;
        }
    }
}
