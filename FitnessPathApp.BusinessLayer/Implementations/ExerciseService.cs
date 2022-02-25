using FitnessPathApp.BusinessLayer.Interfaces;
using FitnessPathApp.DomainLayer.Entities;
using FitnessPathApp.PersistanceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FitnessPathApp.BusinessLayer.Implementations
{
    internal class ExerciseService : IExerciseService
    {
        private readonly IRepository<Exercise> _repository;
        public ExerciseService(IRepository<Exercise> repository)
        {
            _repository = repository;
        }

        public async Task<Exercise> Create(Exercise exercise, CancellationToken cancellationToken)
        {
            await _repository.Insert(exercise, cancellationToken);
            return exercise;
        }

        public async Task<Guid> Delete(Guid id, CancellationToken cancellationToken)
        {
            await _repository.Delete(id, cancellationToken);
            return id;
        }

        public async Task<Exercise> Get(Guid id, CancellationToken cancellationToken)
        {
            var exercise = await _repository.Get(
                filter: dbExercise => dbExercise.Id == id,
                cancellationToken: cancellationToken);

            return exercise;
        }

        public async Task<IEnumerable<Exercise>> GetAll(CancellationToken cancellationToken)
        {
            var exercises = await _repository.GetAll(cancellationToken: cancellationToken);

            return exercises;
        }

        public async Task<Exercise> Update(Exercise exercise, CancellationToken cancellationToken)
        {
            await _repository.Update(exercise, cancellationToken);

            return exercise;
        }
    }
}
