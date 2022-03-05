using FitnessPathApp.BusinessLayer.Interfaces;
using FitnessPathApp.DomainLayer.Entities;
using FitnessPathApp.PersistanceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FitnessPathApp.BusinessLayer.Implementations
{
    public class FoodItemService : IFoodItemService
    {
        private readonly IRepository<FoodItem> _repository;

        public FoodItemService(IRepository<FoodItem> repository)
        {
            _repository = repository;
        }

        public async Task<FoodItem> Create(FoodItem item, CancellationToken cancellationToken)
        {
            await _repository.Insert(item, cancellationToken);
            return item;
        }

        public async Task<Guid> Delete(Guid id, CancellationToken cancellationToken)
        {
            await _repository.Delete(id, cancellationToken);
            return id;
        }

        public async Task<FoodItem> Get(Guid id, CancellationToken cancellationToken)
        {
            var item = await _repository.Get(
                filter: dbItem => dbItem.Id == id,
                cancellationToken: cancellationToken);

            return item;
        }

        public async Task<IEnumerable<FoodItem>> GetAll(CancellationToken cancellationToken)
        {
            var items = await _repository.GetAll(cancellationToken: cancellationToken);

            return items;
        }

        public async Task<FoodItem> Update(FoodItem item, CancellationToken cancellationToken)
        {
            await _repository.Update(item, cancellationToken);

            return item;
        }
    }
}
