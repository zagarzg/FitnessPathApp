using FitnessPathApp.BusinessLayer.Interfaces;
using FitnessPathApp.BusinessLayer.Validators;
using FitnessPathApp.DomainLayer.Entities;
using FitnessPathApp.PersistanceLayer.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using FluentValidation.Results;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using FitnessPathApp.BusinessLayer.Exceptions;
using AutoMapper;
using FitnessPathApp.PersistanceLayer.DTOs;

[assembly: InternalsVisibleTo("FitnessPathApp.Tests")]

namespace FitnessPathApp.BusinessLayer.Implementations
{
    public class ExerciseChoiceService : IExerciseChoiceService
    {
        private readonly IRepository<ExerciseChoice> _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<ExerciseChoiceService> _logger;
        private readonly ExerciseChoiceValidator _validator = new ExerciseChoiceValidator();

        public ExerciseChoiceService(IRepository<ExerciseChoice> repository, ILogger<ExerciseChoiceService> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ExerciseChoiceDTO> Create(ExerciseChoice exerciseChoice, CancellationToken cancellationToken)
        {
            ValidationResult result = _validator.Validate(exerciseChoice);

            if (!result.IsValid)
            {
                ValidationException exception = new ValidationException(nameof(exerciseChoice));
                foreach (ValidationFailure failure in result.Errors)
                {
                    exception._errors.Add(failure.PropertyName, failure.ErrorMessage);
                }
                throw exception;
            }

            try
            {
                await _repository.Insert(exerciseChoice, cancellationToken);
                var mappedExerciseChoice = _mapper.Map<ExerciseChoiceDTO>(exerciseChoice);
                _logger.LogInformation($"Exercise choice succesfully inserted. Choice id: {exerciseChoice.Id}");
                return mappedExerciseChoice;
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
                _logger.LogInformation($"Exercise choice succesfully deleted. Choice id: {id}");
                return id;
            }
            catch (Exception e)
            {
                throw new DeleteException(id, e);
            }
        }

        public async Task<ExerciseChoiceDTO> Get(Guid id, CancellationToken cancellationToken)
        {
            var exerciseChoice = await _repository.Get(
                filter: dbExercise => dbExercise.Id == id,
                cancellationToken: cancellationToken);

            if (exerciseChoice == null)
            {
                throw new NotFoundException(id);
            }

            var mappedExerciseChoice = _mapper.Map<ExerciseChoiceDTO>(exerciseChoice);
            _logger.LogInformation($"Exercise choice succesfully fetched. Choice id: {id}");
            return mappedExerciseChoice;
        }

        public async Task<IEnumerable<ExerciseChoiceDTO>> GetAll(CancellationToken cancellationToken)
        {
            var exerciseChoices = await _repository.GetAll(cancellationToken: cancellationToken);

            var mappedExercise = _mapper.Map<IEnumerable<ExerciseChoiceDTO>>(exerciseChoices);

            return mappedExercise;
        }

        public async Task<ExerciseChoiceDTO> Update(ExerciseChoice exerciseChoice, CancellationToken cancellationToken)
        {
            ValidationResult result = _validator.Validate(exerciseChoice);

            if (!result.IsValid)
            {
                ValidationException exception = new ValidationException(nameof(exerciseChoice));
                foreach (ValidationFailure failure in result.Errors)
                {
                    exception._errors.Add(failure.PropertyName, failure.ErrorMessage);
                }

                throw exception;
            }

            try
            {
                await _repository.Update(exerciseChoice, cancellationToken);
                var mappedExerciseChoice = _mapper.Map<ExerciseChoiceDTO>(exerciseChoice);
                _logger.LogInformation($"Exercise choice succesfully updated. Choice id: {exerciseChoice.Id}");
                return mappedExerciseChoice;
            }
            catch (Exception e)
            {
                throw new UpdateException(exerciseChoice.Id, e);
            }
        }
    }
}
