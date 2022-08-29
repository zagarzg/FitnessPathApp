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
    public class FoodItemService : IFoodItemService
    {
        private readonly IRepository<FoodItem> _repository;
        private readonly ILogger<FoodItemService> _logger;
        private readonly FoodItemValidator _validator = new FoodItemValidator();

        public FoodItemService(IRepository<FoodItem> repository, ILogger<FoodItemService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<FoodItem> Create(FoodItem item, CancellationToken cancellationToken)
        {
            ValidationResult result = _validator.Validate(item);

            if (!result.IsValid)
            {
                ValidationException exception = new ValidationException(nameof(item));
                foreach (ValidationFailure failure in result.Errors)
                {
                    exception._errors.Add(failure.PropertyName, failure.ErrorMessage);
                }
                throw exception;
            }

            try
            {
                await _repository.Insert(item, cancellationToken);
                _logger.LogInformation($"FoodItem succesfully inserted. Log id: {item.Id}");
                return item;
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
                _logger.LogInformation($"FoodItem succesfully deleted. Log id: {id}");
                return id;
            }
            catch(Exception e)
            {
                throw new DeleteException(id, e);
            }
        }

        public async Task<FoodItem> Get(Guid id, CancellationToken cancellationToken)
        {
            var item = await _repository.Get(
                filter: dbItem => dbItem.Id == id,
                cancellationToken: cancellationToken);

            if(item == null)
            {
                throw new NotFoundException(id);
            }

            _logger.LogInformation($"FoodItem succesfully fetched. Log id: {id}");
            return item;
        }

        public async Task<IEnumerable<FoodItem>> GetAll(CancellationToken cancellationToken)
        {
            var items = await _repository.GetAll(cancellationToken: cancellationToken);

            return items;
        }

        public async Task<FoodItem> Update(FoodItem item, CancellationToken cancellationToken)
        {
            ValidationResult result = _validator.Validate(item);

            if (!result.IsValid)
            {
                ValidationException exception = new ValidationException(nameof(item));
                foreach (ValidationFailure failure in result.Errors)
                {
                    exception._errors.Add(failure.PropertyName, failure.ErrorMessage);
                }

                throw exception;
            }

            try
            {
                await _repository.Update(item, cancellationToken);
                _logger.LogInformation($"FoodItem succesfully updated. Log id: {item.Id}");
                return item;
            }
            catch (Exception e)
            {
                throw new UpdateException(item.Id, e);
            }
        }
    }
}
