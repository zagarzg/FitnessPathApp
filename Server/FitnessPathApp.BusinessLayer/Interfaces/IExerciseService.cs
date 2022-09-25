using FitnessPathApp.DomainLayer.Entities;
using FitnessPathApp.PersistanceLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FitnessPathApp.BusinessLayer.Interfaces
{
    public interface IExerciseService
    {
        Task<IEnumerable<ExerciseDTO>> GetAll(CancellationToken cancellationToken);
        Task<ExerciseDTO> Get(Guid id, CancellationToken cancellationToken);
        Task<ExerciseDTO> Create(Exercise exercise, CancellationToken cancellationToken);
        Task<ExerciseDTO> Update(Exercise exercise, CancellationToken cancellationToken);
        Task<Guid> Delete(Guid id, CancellationToken cancellationToken);
    }
}
