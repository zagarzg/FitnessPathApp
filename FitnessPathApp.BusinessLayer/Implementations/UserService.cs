using FitnessPathApp.BusinessLayer.Exceptions;
using FitnessPathApp.BusinessLayer.Interfaces;
using FitnessPathApp.BusinessLayer.Validators;
using FitnessPathApp.DomainLayer.Entities;
using FitnessPathApp.PersistanceLayer.Interfaces;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FitnessPathApp.BusinessLayer.Implementations
{
    internal class UserService : IUserService
    {
        private readonly IRepository<User> _repository;
        private readonly UserValidator _validator = new UserValidator();

        public UserService(IRepository<User> repository)
        {
            _repository = repository;
        }

        public async Task<User> Create(User user, CancellationToken cancellationToken)
        {
            ValidationResult result = _validator.Validate(user);

            if (!result.IsValid)
            {
                ValidationException exception = new ValidationException(nameof(user));
                foreach (ValidationFailure failure in result.Errors)
                {
                    exception._errors.Add(failure.PropertyName, failure.ErrorMessage);
                }
                throw exception;
            }

            try
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

                await _repository.Insert(user, cancellationToken);
                return user;
            }
            catch (Exception e)
            {
                throw new CreateException(e);
            }

            
        }

        public async Task<Guid> Delete(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                await _repository.Delete(id, cancellationToken);
                return id;
            }
            catch (Exception e)
            {
                throw new DeleteException(id, e);
            }
        }

        public async Task<User> Get(Guid id, CancellationToken cancellationToken)
        {
            var user = await _repository.Get(
                filter: dbUser => dbUser.Id == id,
                cancellationToken: cancellationToken);

            if(user == null)
            {
                throw new NotFoundException(id);
            }

            return user;
        }

        public async Task<IEnumerable<User>> GetAll(CancellationToken cancellationToken)
        {
            var users = await _repository.GetAll(
                include: source =>
                    source.Include(user => user.WeightLogs)
                          .Include(user => user.TrainingLogs).ThenInclude(log => log.Exercises)
                          .Include(user => user.FoodLogs).ThenInclude(log => log.FoodItems),
                cancellationToken: cancellationToken);

            return users;
        }

        public async Task<User> Update(User user, CancellationToken cancellationToken)
        {
            ValidationResult result = _validator.Validate(user);

            if (!result.IsValid)
            {
                ValidationException exception = new ValidationException(nameof(user));
                foreach (ValidationFailure failure in result.Errors)
                {
                    exception._errors.Add(failure.PropertyName, failure.ErrorMessage);
                }

                throw exception;
            }

            try
            {
                await _repository.Update(user, cancellationToken);
                return user;
            }
            catch (Exception e)
            {
                throw new UpdateException(user.Id, e);
            }
        }
    }
}
