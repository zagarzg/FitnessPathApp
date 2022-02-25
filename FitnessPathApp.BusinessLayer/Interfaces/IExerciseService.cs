using FitnessPathApp.DomainLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FitnessPathApp.BusinessLayer.Interfaces
{
    public interface IExerciseService
    {
        Task<IEnumerable<Exercise>> GetAll(CancellationToken cancellationToken);
        Task<Exercise> Get(Guid id, CancellationToken cancellationToken);
        Task<Exercise> Create(Exercise exercise, CancellationToken cancellationToken);
        Task<Exercise> Update(Exercise exercise, CancellationToken cancellationToken);
        Task<Guid> Delete(Guid id, CancellationToken cancellationToken);
    }
}
