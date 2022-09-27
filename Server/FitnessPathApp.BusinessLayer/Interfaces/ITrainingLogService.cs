using FitnessPathApp.DomainLayer.Entities;
using FitnessPathApp.PersistanceLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FitnessPathApp.BusinessLayer.Interfaces
{
    public interface ITrainingLogService
    {
        Task<IEnumerable<TrainingLogDTO>> GetAll(CancellationToken cancellationToken);
        Task<IEnumerable<TrainingLogDTO>> GetMonthlyTrainingLogs(int month, CancellationToken cancellationToken);
        Task<TrainingLogDTO> Get(Guid id, CancellationToken cancellationToken);
        Task<TrainingLogDTO> Create(TrainingLog log, CancellationToken cancellationToken);
        Task<TrainingLogDTO> Update(TrainingLog log, CancellationToken cancellationToken);
        Task<Guid> Delete(Guid id, CancellationToken cancellationToken);
    }
}
