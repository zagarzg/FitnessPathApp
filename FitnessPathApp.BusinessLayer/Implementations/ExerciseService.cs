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
    internal class ExerciseService : IExerciseService
    {
        private readonly IRepository<Exercise> _repository;
        private readonly ILogger<ExerciseService> _logger;
        private readonly ExerciseValidator _validator = new ExerciseValidator();
        public ExerciseService(IRepository<Exercise> repository, ILogger<ExerciseService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<Exercise> Create(Exercise exercise, CancellationToken cancellationToken)
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
                _logger.LogInformation($"Exercise succesfully inserted. Exercise id: {exercise.Id}");
                return exercise;
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

        public async Task<Exercise> Get(Guid id, CancellationToken cancellationToken)
        {
            var exercise = await _repository.Get(
                filter: dbExercise => dbExercise.Id == id,
                cancellationToken: cancellationToken);

            if (exercise == null)
            {
                throw new NotFoundException(id);
            }

            _logger.LogInformation($"Exercise succesfully fetched. Exercise id: {id}");
            return exercise;
        }

        public async Task<IEnumerable<Exercise>> GetAll(CancellationToken cancellationToken)
        {
            var exercises = await _repository.GetAll(cancellationToken: cancellationToken);

            return exercises;
        }

        public async Task<Exercise> Update(Exercise exercise, CancellationToken cancellationToken)
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
                _logger.LogInformation($"Exercise succesfully updated. Exercise id: {exercise.Id}");
                return exercise;
            }
            catch (Exception e)
            {
                throw new UpdateException(exercise.Id, e);
            }
            
        }
    }
}
