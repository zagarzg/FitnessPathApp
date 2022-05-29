using FitnessPathApp.DomainLayer.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FitnessPathApp.BusinessLayer.Interfaces
{
    public interface IWeightLogService
    {
        Task<IEnumerable<WeightLog>> GetAll(CancellationToken cancellationToken);
        Task<WeightLog> Get(Guid id, CancellationToken cancellationToken);
        Task<WeightLog> Create(WeightLog log, CancellationToken cancellationToken);
        Task<WeightLog> Update(WeightLog log, CancellationToken cancellationToken);
        Task<Guid> Delete(Guid id, CancellationToken cancellationToken);
    }
}
