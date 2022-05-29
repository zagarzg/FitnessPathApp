using FitnessPathApp.DomainLayer.Entities;
using FluentValidation;

namespace FitnessPathApp.BusinessLayer.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator() 
        {
            RuleFor(user => user.Username)
                .NotEmpty()
                .WithMessage("Username must be set");

            RuleFor(user => user.Password)
                .NotEmpty()
                .WithMessage("Password must be set");

            RuleFor(user => user.Email)
                .NotEmpty()
                .WithMessage("Email must be set");
        }
    }
}
