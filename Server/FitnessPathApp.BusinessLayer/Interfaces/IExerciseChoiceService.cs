using FitnessPathApp.DomainLayer.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FitnessPathApp.BusinessLayer.Interfaces
{
    public interface IExerciseChoiceService
    {
        Task<IEnumerable<ExerciseChoice>> GetAll(CancellationToken cancellationToken);
        Task<ExerciseChoice> Get(Guid id, CancellationToken cancellationToken);
        Task<ExerciseChoice> Create(ExerciseChoice exerciseChoice, CancellationToken cancellationToken);
        Task<ExerciseChoice> Update(ExerciseChoice exerciseChoice, CancellationToken cancellationToken);
        Task<Guid> Delete(Guid id, CancellationToken cancellationToken);
    }
}
