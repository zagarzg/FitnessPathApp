using FitnessPathApp.BusinessLayer.Interfaces;
using FitnessPathApp.DomainLayer.Entities;
using FitnessPathApp.PersistanceLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FitnessPathApp.BusinessLayer.Implementations
{
    internal class TrainingLogService : ITrainingLogService
    {
        private readonly IRepository<TrainingLog> _repository;
        public TrainingLogService(IRepository<TrainingLog> repository)
        {
            _repository = repository;
        }

        public async Task<TrainingLog> Create(TrainingLog log, CancellationToken cancellationToken)
        {
            await _repository.Insert(log, cancellationToken);
            return log;
        }

        public async Task<Guid> Delete(Guid id, CancellationToken cancellationToken)
        {
            await _repository.Delete(id, cancellationToken);
            return id;
        }

        public async Task<TrainingLog> Get(Guid id, CancellationToken cancellationToken)
        {
            var log = await _repository.Get(
                include: source => source.Include(log => log.Exercises),
                filter: dbLog => dbLog.Id == id,
                cancellationToken: cancellationToken);

            return log;
        }

        public async Task<IEnumerable<TrainingLog>> GetAll(CancellationToken cancellationToken)
        {
            var logs = await _repository.GetAll(
                include: source => source.Include(log => log.Exercises),
                cancellationToken: cancellationToken);

            return logs;
        }

        public async Task<TrainingLog> Update(TrainingLog log, CancellationToken cancellationToken)
        {
            await _repository.Update(log, cancellationToken);

            return log;
        }
    }
}
