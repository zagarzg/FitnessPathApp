using FitnessPathApp.DomainLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FitnessPathApp.BusinessLayer.Interfaces
{
    public interface IFoodLogService
    {
        Task<IEnumerable<FoodLog>> GetAll(CancellationToken cancellationToken);
        Task<FoodLog> Get(Guid id, CancellationToken cancellationToken);
        Task<FoodLog> Create(FoodLog log, CancellationToken cancellationToken);
        Task<FoodLog> Update(FoodLog log, CancellationToken cancellationToken);
        Task<Guid> Delete(Guid id, CancellationToken cancellationToken);
    }
}
