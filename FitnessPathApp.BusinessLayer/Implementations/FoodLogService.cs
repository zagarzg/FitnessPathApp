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
using System.Threading;
using System.Threading.Tasks;

namespace FitnessPathApp.BusinessLayer.Implementations
{
    public class FoodLogService : IFoodLogService
    {
        private readonly IRepository<FoodLog> _repository;
        private readonly ILogger<FoodLogService> _logger;
        private readonly FoodLogValidator _validator = new FoodLogValidator();
        public FoodLogService(IRepository<FoodLog> repository, ILogger<FoodLogService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<FoodLog> Create(FoodLog log, CancellationToken cancellationToken)
        {
            ValidationResult result = _validator.Validate(log);

            if (!result.IsValid)
            {
                ValidationException exception = new ValidationException(nameof(log));
                foreach (ValidationFailure failure in result.Errors)
                {
                    exception._errors.Add(failure.PropertyName, failure.ErrorMessage);
                }
                throw exception;
            }

            try
            {
                await _repository.Insert(log, cancellationToken);
                _logger.LogInformation($"FoodLog succesfully inserted. Log id: {log.Id}");
                return log;
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
                _logger.LogInformation($"FoodLog succesfully deleted. Log id: {id}");
                return id;
            }
            catch(Exception e)
            {
                throw new DeleteException(id, e);
            }

            
        }

        public async Task<FoodLog> Get(Guid id, CancellationToken cancellationToken)
        {
            var log = await _repository.Get(
                include: source => source.Include(log => log.FoodItems),
                filter: dbLog => dbLog.Id == id,
                cancellationToken: cancellationToken);

            if(log == null)
            {
                throw new NotFoundException(id);
            }

            _logger.LogInformation($"FoodLog succesfully fetched. Log id: {id}");
            return log;
        }

        public async Task<IEnumerable<FoodLog>> GetAll(CancellationToken cancellationToken)
        {
            var logs = await _repository.GetAll(
                include: source => source.Include(log => log.FoodItems),
                cancellationToken: cancellationToken);

            return logs;
        }

        public async Task<FoodLog> Update(FoodLog log, CancellationToken cancellationToken)
        {
            ValidationResult result = _validator.Validate(log);

            if (!result.IsValid)
            {
                ValidationException exception = new ValidationException(nameof(log));
                foreach (ValidationFailure failure in result.Errors)
                {
                    exception._errors.Add(failure.PropertyName, failure.ErrorMessage);
                }

                throw exception;
            }

            try
            {
                await _repository.Update(log, cancellationToken);
                _logger.LogInformation($"FoodLog succesfully updated. Log id: {log.Id}");
                return log;
            }
            catch (Exception e)
            {
                throw new UpdateException(log.Id, e);
            }
        }
    }
}
