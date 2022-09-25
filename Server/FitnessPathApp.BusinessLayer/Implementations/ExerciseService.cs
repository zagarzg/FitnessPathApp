using AutoMapper;
using FitnessPathApp.BusinessLayer.Exceptions;
using FitnessPathApp.BusinessLayer.Interfaces;
using FitnessPathApp.BusinessLayer.Validators;
using FitnessPathApp.DomainLayer.Entities;
using FitnessPathApp.PersistanceLayer.DTOs;
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
    internal class ExerciseService : IExerciseService
    {
        private readonly IRepository<Exercise> _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<ExerciseService> _logger;
        private readonly ExerciseValidator _validator = new ExerciseValidator();
        public ExerciseService(IRepository<Exercise> repository, ILogger<ExerciseService> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ExerciseDTO> Create(Exercise exercise, CancellationToken cancellationToken)
        {
            ValidationResult result = _validator.Validate(exercise);

            if (!result.IsValid)
            {
                ValidationException exception = new ValidationException(nameof(exercise));
                foreach (ValidationFailure failure in result.Errors)
                {
                    exception._errors.Add(failure.PropertyName, failure.ErrorMessage);
                }
                throw exception;
            }

            try
            {
                await _repository.Insert(exercise, cancellationToken);
                var mappedExercise = _mapper.Map<ExerciseDTO>(exercise);
                _logger.LogInformation($"Exercise succesfully inserted. Exercise id: {exercise.Id}");
                return mappedExercise;
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
                _logger.LogInformation($"Exercise succesfully deleted. Exercise id: {id}");
                return id;
            }
            catch(Exception e)
            {
                throw new DeleteException(id, e);
            }
        }

        public async Task<ExerciseDTO> Get(Guid id, CancellationToken cancellationToken)
        {
            var exercise = await _repository.Get(
                filter: dbExercise => dbExercise.Id == id,
                cancellationToken: cancellationToken);

            if (exercise == null)
            {
                throw new NotFoundException(id);
            }

            var mappedExercise = _mapper.Map<ExerciseDTO>(exercise);
            _logger.LogInformation($"Exercise succesfully fetched. Exercise id: {id}");
            return mappedExercise;
        }

        public async Task<IEnumerable<ExerciseDTO>> GetAll(CancellationToken cancellationToken)
        {
            var exercises = await _repository.GetAll(cancellationToken: cancellationToken);

            var mappedExercise = _mapper.Map<IEnumerable<ExerciseDTO>>(exercises);

            return mappedExercise;
        }

        public async Task<ExerciseDTO> Update(Exercise exercise, CancellationToken cancellationToken)
        {
            ValidationResult result = _validator.Validate(exercise);

            if (!result.IsValid)
            {
                ValidationException exception = new ValidationException(nameof(exercise));
                foreach (ValidationFailure failure in result.Errors)
                {
                    exception._errors.Add(failure.PropertyName, failure.ErrorMessage);
                }

                throw exception;
            }

            try
            {
                await _repository.Update(exercise, cancellationToken);
                var mappedExercise = _mapper.Map<ExerciseDTO>(exercise);
                _logger.LogInformation($"Exercise succesfully updated. Exercise id: {exercise.Id}");
                return mappedExercise;
            }
            catch (Exception e)
            {
                throw new UpdateException(exercise.Id, e);
            }
            
        }
    }
}
