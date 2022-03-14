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
    internal class UserService : IUserService
    {
        private readonly IRepository<User> _repository;

        public UserService(IRepository<User> repository)
        {
            _repository = repository;
        }

        public async Task<User> Create(User user, CancellationToken cancellationToken)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            await _repository.Insert(user, cancellationToken);
            return user;
        }

        public async Task<Guid> Delete(Guid id, CancellationToken cancellationToken)
        {
            await _repository.Delete(id, cancellationToken);
            return id;
        }

        public async Task<User> Get(Guid id, CancellationToken cancellationToken)
        {
            var user = await _repository.Get(
                filter: dbUser => dbUser.Id == id,
                cancellationToken: cancellationToken);

            return user;
        }

        public async Task<IEnumerable<User>> GetAll(CancellationToken cancellationToken)
        {
            var users = await _repository.GetAll(cancellationToken: cancellationToken);

            return users;
        }

        public async Task<User> Update(User user, CancellationToken cancellationToken)
        {
            await _repository.Update(user, cancellationToken);

            return user;
        }
    }
}
