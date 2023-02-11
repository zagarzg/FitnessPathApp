using FitnessPathApp.DomainLayer.Entities;
using FitnessPathApp.PersistanceLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FitnessPathApp.BusinessLayer.Interfaces
{
    public interface IWeightLogService
    {
        Task<IEnumerable<WeightLogDTO>> GetAll(CancellationToken cancellationToken);
        Task<WeightLogDTO> Get(Guid id, CancellationToken cancellationToken);
        Task<WeightLogDTO> Create(WeightLog log, CancellationToken cancellationToken);
        Task<WeightLogDTO> Update(WeightLog log, CancellationToken cancellationToken);
        Task<Guid> Delete(Guid id, CancellationToken cancellationToken);
    }
}
