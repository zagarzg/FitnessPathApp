using FitnessPathApp.DomainLayer.Entities;
using FitnessPathApp.PersistanceLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FitnessPathApp.BusinessLayer.Interfaces
{
    public interface IExerciseChoiceService
    {
        Task<IEnumerable<ExerciseChoiceDTO>> GetAll(CancellationToken cancellationToken);
        Task<ExerciseChoiceDTO> Get(Guid id, CancellationToken cancellationToken);
        Task<ExerciseChoiceDTO> Create(ExerciseChoice exerciseChoice, CancellationToken cancellationToken);
        Task<ExerciseChoiceDTO> Update(ExerciseChoice exerciseChoice, CancellationToken cancellationToken);
        Task<Guid> Delete(Guid id, CancellationToken cancellationToken);
    }
}
