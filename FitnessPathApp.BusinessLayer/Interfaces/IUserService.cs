using FitnessPathApp.DomainLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FitnessPathApp.BusinessLayer.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAll(CancellationToken cancellationToken);
        Task<User> Get(Guid id, CancellationToken cancellationToken);
        Task<User> Create(User user, CancellationToken cancellationToken);
        Task<User> Update(User user, CancellationToken cancellationToken);
        Task<Guid> Delete(Guid id, CancellationToken cancellationToken);
    }
}
