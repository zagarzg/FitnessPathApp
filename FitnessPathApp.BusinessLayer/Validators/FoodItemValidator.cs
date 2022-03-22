using FitnessPathApp.DomainLayer.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessPathApp.BusinessLayer.Validators
{
    public class FoodItemValidator : AbstractValidator<FoodItem>
    {
        public FoodItemValidator()
        {
            RuleFor(item => item.Name)
               .NotEmpty()
               .WithMessage("Exercise name must be set");

            RuleFor(item => item.Calories)
                .NotEmpty()
                .WithMessage("Calories must be set")
                .GreaterThan(0)
                .WithMessage("Calories must be greater then 0");

            RuleFor(item => item.Carbs)
                .NotEmpty()
                .WithMessage("Carbs must be set")
                .GreaterThan(0)
                .WithMessage("Carbs must be greater then 0");

            RuleFor(item => item.Protein)
                .NotEmpty()
                .WithMessage("Protein must be set")
                .GreaterThan(0)
                .WithMessage("Protein must be greater then 0");

            RuleFor(item => item.Fat)
                .NotEmpty()
                .WithMessage("Fat must be set")
                .GreaterThan(0)
                .WithMessage("Fat must be greater then 0");

            RuleFor(item => item.FoodLogId)
                .NotEmpty()
                .WithMessage("FoodItem must be connected to specific FoodLog");
        }
    }
}
