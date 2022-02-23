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
    internal class WeightLogService : IWeightLogService
    {
        private readonly IRepository<WeightLog> _repository;

        public WeightLogService(IRepository<WeightLog> repository)
        {
            _repository = repository;
        }

        public async Task<WeightLog> Create(WeightLog log, CancellationToken cancellationToken)
        {
            await _repository.Insert(log, cancellationToken);
            return log;
        }

        public async Task<Guid> Delete(Guid id, CancellationToken cancellationToken)
        {
            await _repository.Delete(id, cancellationToken);
            return id;
        }

        public async Task<WeightLog> Get(Guid id, CancellationToken cancellationToken)
        {
            var log = await _repository.Get(
                filter: dbLog => dbLog.Id == id,
                cancellationToken: cancellationToken);

            return log;
        }

        public async Task<IEnumerable<WeightLog>> GetAll(CancellationToken cancellationToken)
        {
            var logs = await _repository.GetAll(cancellationToken: cancellationToken);

            return logs;
        }

        public async Task<WeightLog> Update(WeightLog log, CancellationToken cancellationToken)
        {
            await _repository.Update(log, cancellationToken);

            return log;
        }
    }
}
