using FitnessPathApp.DomainLayer.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FitnessPathApp.BusinessLayer.Interfaces
{
    public interface ITrainingLogService
    {
        Task<IEnumerable<TrainingLog>> GetAll(CancellationToken cancellationToken);
        Task<TrainingLog> Get(Guid id, CancellationToken cancellationToken);
        Task<TrainingLog> Create(TrainingLog log, CancellationToken cancellationToken);
        Task<TrainingLog> Update(TrainingLog log, CancellationToken cancellationToken);
        Task<Guid> Delete(Guid id, CancellationToken cancellationToken);
    }
}
