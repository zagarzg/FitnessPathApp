using AutoMapper;
using FitnessPathApp.BusinessLayer.Exceptions;
using FitnessPathApp.BusinessLayer.Interfaces;
using FitnessPathApp.BusinessLayer.Validators;
using FitnessPathApp.DomainLayer.Entities;
using FitnessPathApp.PersistanceLayer.DTOs;
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
    public class FoodLogService : IFoodLogService
    {
        private readonly IRepository<FoodLog> _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<FoodLogService> _logger;
        private readonly FoodLogValidator _validator = new FoodLogValidator();
        public FoodLogService(IRepository<FoodLog> repository, ILogger<FoodLogService> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<FoodLogDTO> Create(FoodLog log, CancellationToken cancellationToken)
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
                var mappedFoodLog = _mapper.Map<FoodLogDTO>(log);
                _logger.LogInformation($"FoodLog succesfully inserted. Log id: {log.Id}");
                return mappedFoodLog;
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

        public async Task<FoodLogDTO> Get(Guid id, CancellationToken cancellationToken)
        {
            var log = await _repository.Get(
                include: source => source.Include(log => log.FoodItems),
                filter: dbLog => dbLog.Id == id,
                cancellationToken: cancellationToken);

            if(log == null)
            {
                throw new NotFoundException(id);
            }

            var mappedFoodLog = _mapper.Map<FoodLogDTO>(log);
            _logger.LogInformation($"FoodLog succesfully fetched. Log id: {id}");
            return mappedFoodLog;
        }

        public async Task<IEnumerable<FoodLogDTO>> GetAll(CancellationToken cancellationToken)
        {
            var logs = await _repository.GetAll(
                include: source => source.Include(log => log.FoodItems),
                cancellationToken: cancellationToken);

            var mappedFoodLogs = _mapper.Map<IEnumerable<FoodLogDTO>>(logs);

            return mappedFoodLogs;
        }

        public async Task<FoodLogDTO> Update(FoodLog log, CancellationToken cancellationToken)
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
                var mappedFoodLog = _mapper.Map<FoodLogDTO>(log);
                _logger.LogInformation($"FoodLog succesfully updated. Log id: {log.Id}");
                return mappedFoodLog;
            }
            catch (Exception e)
            {
                throw new UpdateException(log.Id, e);
            }
        }
    }
}
