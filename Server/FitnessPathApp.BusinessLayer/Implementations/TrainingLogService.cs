﻿using AutoMapper;
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
    internal class TrainingLogService : ITrainingLogService
    {
        private readonly IRepository<TrainingLog> _repository;
        private readonly ILogger<TrainingLogService> _logger;
        private readonly IMapper _mapper;
        private readonly TrainingLogValidator _validator = new TrainingLogValidator();
        public TrainingLogService(IRepository<TrainingLog> repository, ILogger<TrainingLogService> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<TrainingLogDTO> Create(TrainingLog log, CancellationToken cancellationToken)
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
                var mappedLog = _mapper.Map<TrainingLogDTO>(log);
                _logger.LogInformation($"Training succesfully inserted. Log id: {log.Id}");
                return mappedLog;
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
                _logger.LogInformation($"Training succesfully deleted. Log id: {id}");
                return id;
            }
            catch (Exception e)
            {
                throw new DeleteException(id, e);
            }
        }

        public async Task<TrainingLogDTO> Get(Guid id, CancellationToken cancellationToken)
        {
            var log = await _repository.Get(
                include: source => source.Include(log => log.Exercises),
                filter: dbLog => dbLog.Id == id,
                cancellationToken: cancellationToken);

            if (log == null)
            {
                throw new NotFoundException(id);
            }

            var mappedLog = _mapper.Map<TrainingLogDTO>(log);

            _logger.LogInformation($"Training succesfully fetched. Log id: {id}");
            return mappedLog;
        }

        public async Task<IEnumerable<TrainingLogDTO>> GetAll(CancellationToken cancellationToken)
        {
            var logs = await _repository.GetAll(
                include: source => source.Include(log => log.Exercises),
                cancellationToken: cancellationToken);

            var mappedLogs = _mapper.Map<IEnumerable<TrainingLogDTO>>(logs);

            return mappedLogs;
        }

        public async Task<TrainingLogDTO> Update(TrainingLog log, CancellationToken cancellationToken)
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
                var mappedLog = _mapper.Map<TrainingLogDTO>(log);
                _logger.LogInformation($"Training succesfully updated. Log id: {log.Id}");
                return mappedLog;
            }
            catch (Exception e)
            {
                throw new UpdateException(log.Id, e);
            }
        }
    }
}
