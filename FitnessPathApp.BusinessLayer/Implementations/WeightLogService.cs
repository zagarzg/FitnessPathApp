using FitnessPathApp.BusinessLayer.Interfaces;
using FitnessPathApp.DomainLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPathApp.BusinessLayer.Implementations
{
    internal class WeightLogService : IWeightLogService
    {
        public Task<WeightLog> Create(WeightLog log)
        {
            throw new NotImplementedException($"Creating {log.Id}");
        }

        public Task Delete(Guid id)
        {
            throw new NotImplementedException($"Deleting {id}");
        }

        public Task<WeightLog> Get(Guid id)
        {
            throw new NotImplementedException($"Getting {id}");
        }

        public Task<IEnumerable<WeightLog>> GetAll()
        {
            throw new NotImplementedException("Getting all");
        }

        public Task Update(WeightLog log)
        {
            throw new NotImplementedException($"Updating {log.Id}");
        }
    }
}
