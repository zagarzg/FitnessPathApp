using FitnessPathApp.DomainLayer.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FitnessPathApp.BusinessLayer.Interfaces
{
    public interface IFoodItemService
    {
        Task<IEnumerable<FoodItem>> GetAll(CancellationToken cancellationToken);
        Task<FoodItem> Get(Guid id, CancellationToken cancellationToken);
        Task<FoodItem> Create(FoodItem item, CancellationToken cancellationToken);
        Task<FoodItem> Update(FoodItem item, CancellationToken cancellationToken);
        Task<Guid> Delete(Guid id, CancellationToken cancellationToken);
    }
}
