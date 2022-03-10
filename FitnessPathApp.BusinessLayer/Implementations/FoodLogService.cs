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
    public class FoodLogService : IFoodLogService
    {
        private readonly IRepository<FoodLog> _repository;
        public FoodLogService(IRepository<FoodLog> repository)
        {
            _repository = repository;
        }

        public async Task<FoodLog> Create(FoodLog log, CancellationToken cancellationToken)
        {
            await _repository.Insert(log, cancellationToken);
            return log;
        }

        public async Task<Guid> Delete(Guid id, CancellationToken cancellationToken)
        {
            await _repository.Delete(id, cancellationToken);
            return id;
        }

        public async Task<FoodLog> Get(Guid id, CancellationToken cancellationToken)
        {
            var log = await _repository.Get(
                include: source => source.Include(log => log.FoodItems),
                filter: dbLog => dbLog.Id == id,
                cancellationToken: cancellationToken);

            return log;
        }

        public async Task<IEnumerable<FoodLog>> GetAll(CancellationToken cancellationToken)
        {
            var logs = await _repository.GetAll(
                include: source => source.Include(log => log.FoodItems),
                cancellationToken: cancellationToken);

            return logs;
        }

        public async Task<FoodLog> Update(FoodLog log, CancellationToken cancellationToken)
        {
            await _repository.Update(log, cancellationToken);

            return log;
        }
    }
}
