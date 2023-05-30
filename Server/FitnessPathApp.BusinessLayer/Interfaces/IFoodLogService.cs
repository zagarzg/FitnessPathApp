using FitnessPathApp.DomainLayer.Entities;
using FitnessPathApp.PersistanceLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FitnessPathApp.BusinessLayer.Interfaces
{
    public interface IFoodLogService
    {
        Task<IEnumerable<FoodLogDTO>> GetAll(CancellationToken cancellationToken);
        Task<FoodLogDTO> Get(Guid id, CancellationToken cancellationToken);
        Task<FoodLogDTO> Create(FoodLog log, CancellationToken cancellationToken);
        Task<FoodLogDTO> Update(FoodLog log, CancellationToken cancellationToken);
        Task<Guid> Delete(Guid id, CancellationToken cancellationToken);
    }
}
