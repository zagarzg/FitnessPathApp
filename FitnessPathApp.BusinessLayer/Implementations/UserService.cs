using FitnessPathApp.BusinessLayer.Exceptions;
using FitnessPathApp.BusinessLayer.Interfaces;
using FitnessPathApp.BusinessLayer.Validators;
using FitnessPathApp.DomainLayer.Entities;
using FitnessPathApp.PersistanceLayer.Interfaces;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("FitnessPathApp.Tests")]

namespace FitnessPathApp.BusinessLayer.Implementations
{
    internal class UserService : IUserService
    {
        private readonly IRepository<User> _repository;
        private readonly ILogger<UserService> _logger;
        private readonly UserValidator _validator = new UserValidator();

        public UserService(IRepository<User> repository, ILogger<UserService> logger)
        {
            _repository = repository;
            _logger = logger;
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
                _logger.LogInformation($"User succesfully inserted. User id: {user.Id}");
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
                _logger.LogInformation($"User succesfully deleted. User id: {id}");
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

            _logger.LogInformation($"User succesfully fetched. User id: {id}");
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
                _logger.LogInformation($"User succesfully updated. Log id: {user.Id}");
                return user;
            }
            catch (Exception e)
            {
                throw new UpdateException(user.Id, e);
            }
        }
    }
}
