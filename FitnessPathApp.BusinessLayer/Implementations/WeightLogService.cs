using FitnessPathApp.BusinessLayer.Exceptions;
using FitnessPathApp.BusinessLayer.Interfaces;
using FitnessPathApp.BusinessLayer.Validators;
using FitnessPathApp.DomainLayer.Entities;
using FitnessPathApp.PersistanceLayer.Interfaces;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("FitnessPathApp.Tests")]

namespace FitnessPathApp.BusinessLayer.Implementations
{
    internal class WeightLogService : IWeightLogService
    {
        private readonly IRepository<WeightLog> _repository;
        private readonly ILogger<WeightLogService> _logger;
        private readonly WeightLogValidator _validator = new WeightLogValidator();
        public WeightLogService(IRepository<WeightLog> repository, ILogger<WeightLogService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<WeightLog> Create(WeightLog log, CancellationToken cancellationToken)
        {
            ValidationResult result = _validator.Validate(log);

            if(!result.IsValid)
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
                _logger.LogInformation($"Weight succesfully inserted. Log id: {log.Id}");
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
                _logger.LogInformation($"Weight succesfully deleted. Log id: {id}");
                return id;
            }
            catch(Exception e)
            {
                throw new DeleteException(id, e);
            }

        }

        public async Task<WeightLog> Get(Guid id, CancellationToken cancellationToken)
        {
            var log = await _repository.Get(
                filter: dbLog => dbLog.Id == id,
                cancellationToken: cancellationToken);

            if(log == null)
            {
                throw new NotFoundException(id);
            }

            _logger.LogInformation($"Weight succesfully fetched. Log id: {id}");
            return log;
        }

        public async Task<IEnumerable<WeightLog>> GetAll(CancellationToken cancellationToken)
        {
            var logs = await _repository.GetAll(cancellationToken: cancellationToken);

            return logs;
        }

        public async Task<WeightLog> Update(WeightLog log, CancellationToken cancellationToken)
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
                _logger.LogInformation($"Weight succesfully updated. Log id: {log.Id}");
                return log;
            }
            catch (Exception e)
            {
                throw new UpdateException(log.Id, e);
            }
            
        }
    }
}
