using FitnessPathApp.BusinessLayer.Exceptions;
using FitnessPathApp.BusinessLayer.Interfaces;
using FitnessPathApp.BusinessLayer.Validators;
using FitnessPathApp.DomainLayer.Entities;
using FitnessPathApp.PersistanceLayer.Interfaces;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FitnessPathApp.BusinessLayer.Implementations
{
    internal class WeightLogService : IWeightLogService
    {
        private readonly IRepository<WeightLog> _repository;
        private readonly WeightLogValidator _validator = new WeightLogValidator();
        public WeightLogService(IRepository<WeightLog> repository)
        {
            _repository = repository;
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
                return log;
            }
            catch (Exception e)
            {
                throw new UpdateException(log.Id, e);
            }
            
        }
    }
}
