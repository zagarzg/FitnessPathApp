using FitnessPathApp.DomainLayer.Entities;
using FitnessPathApp.PersistanceLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FitnessPathApp.BusinessLayer.Interfaces
{
    public interface IFoodItemService
    {
        Task<IEnumerable<FoodItemDTO>> GetAll(CancellationToken cancellationToken);
        Task<FoodItemDTO> Get(Guid id, CancellationToken cancellationToken);
        Task<FoodItemDTO> Create(FoodItem item, CancellationToken cancellationToken);
        Task<FoodItemDTO> Update(FoodItem item, CancellationToken cancellationToken);
        Task<Guid> Delete(Guid id, CancellationToken cancellationToken);
    }
}
