using FitnessPathApp.DomainLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPathApp.BusinessLayer.Interfaces
{
    public interface IWeightLogService
    {
        Task<IEnumerable<WeightLog>> GetAll();
        Task<WeightLog> Get(Guid id);
        Task<WeightLog> Create(WeightLog log);
        Task Update(WeightLog log);
        Task Delete(Guid id);
    }
}
