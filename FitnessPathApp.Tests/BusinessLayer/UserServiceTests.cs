using FitnessPathApp.BusinessLayer.Exceptions;
using FitnessPathApp.BusinessLayer.Implementations;
using FitnessPathApp.DomainLayer.Entities;
using FitnessPathApp.PersistanceLayer.Interfaces;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace FitnessPathApp.Tests.BusinessLayer
{
    public class UserServiceTests
    {
        [Fact]
        public async Task GetAll_TwoUsersInDb_GetsBoth()
        {
            var users = new List<User>()
            {
                new User()
                {
                    Id = Guid.Parse("47b176a6-535f-447b-9f09-86465f07967a"),
                    Username = "unittest1",
                    Password = "unittest1",
                    Email = "unittest1@test.com"
                },
                new User()
                {
                    Id = Guid.Parse("d386146c-06ac-4325-82fa-11721ecb1d4b"),
                    Username = "unittest2",
                    Password = "unittest2",
                    Email = "unittest2@test.com"
                },
            };

            // ARRANGE
            var repository = new Mock<IRepository<User>>();
            repository.Setup(x =>
                x.GetAll(
                    null,
                    null,
                    It.IsAny<Func<IQueryable<User>, IIncludableQueryable<User, object>>>(),
                    true,
                    CancellationToken.None
            )).ReturnsAsync(users);

            var logger = new NullLogger<UserService>();

            var service = new UserService(repository: repository.Object, logger: logger);

            // ACT
            var result = (await service.GetAll(CancellationToken.None)).ToList();

            // ASSERT
            Assert.IsType<List<User>>(result);
            Assert.Equal(users[0].Id, result[0].Id);
            Assert.Equal(users[1].Id, result[1].Id);

            repository.Verify(x => x.GetAll(null,
                    null,
                    It.IsAny<Func<IQueryable<User>, IIncludableQueryable<User, object>>>(),
                    true,
                    CancellationToken.None)
            , Times.Once);

            repository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Get_TwoUsersInDb_GetsOne()
        {
            var users = new List<User>()
            {
                new User()
                {
                    Id = Guid.Parse("47b176a6-535f-447b-9f09-86465f07967a"),
                    Username = "unittest1",
                    Password = "unittest1",
                    Email = "unittest1@test.com"
                },
                new User()
                {
                    Id = Guid.Parse("d386146c-06ac-4325-82fa-11721ecb1d4b"),
                    Username = "unittest2",
                    Password = "unittest2",
                    Email = "unittest2@test.com"
                },
            };

            // ARRANGE
            var repository = new Mock<IRepository<User>>();
            var id = users[1].Id;
            repository.Setup(x =>
                x.Get(
                    dbLog => dbLog.Id == id,
                    null,
                    It.IsAny<Func<IQueryable<User>, IIncludableQueryable<User, object>>>(),
                    true,
                    CancellationToken.None
            )).ReturnsAsync(users[1]);

            var logger = new NullLogger<UserService>();

            var service = new UserService(repository: repository.Object, logger: logger);

            // ACT
            var result = await service.Get(id, CancellationToken.None);

            // ASSERT
            Assert.IsType<User>(result);
            Assert.Equal(users[1], result);

            repository.Verify(x => x.Get(
                    dbLog => dbLog.Id == id,
                    null,
                    It.IsAny<Func<IQueryable<User>, IIncludableQueryable<User, object>>>(),
                    true,
                    CancellationToken.None)
            , Times.Once);

            repository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Get_TwoUsersInDb_ThrowsNotFoundException()
        {
            var users = new List<User>()
            {
                new User()
                {
                    Id = Guid.Parse("47b176a6-535f-447b-9f09-86465f07967a"),
                    Username = "unittest1",
                    Password = "unittest1",
                    Email = "unittest1@test.com"
                },
                new User()
                {
                    Id = Guid.Parse("d386146c-06ac-4325-82fa-11721ecb1d4b"),
                    Username = "unittest2",
                    Password = "unittest2",
                    Email = "unittest2@test.com"
                },
            };

            // ARRANGE
            var repository = new Mock<IRepository<User>>();
            var userId = Guid.Parse("689f9a14-de94-424e-945f-54c92518e2e6");

            var exception = new NotFoundException(userId);

            repository.Setup(x =>
                x.Get(
                    dbExercise => dbExercise.Id == userId,
                    null,
                    It.IsAny<Func<IQueryable<User>, IIncludableQueryable<User, object>>>(),
                    true,
                    CancellationToken.None
            )).ReturnsAsync((User)null);

            var logger = new NullLogger<UserService>();

            var service = new UserService(repository: repository.Object, logger: logger);

            // ACT/ASSERT
            await Assert.ThrowsAsync<NotFoundException>(() => service.Get(userId, CancellationToken.None));

            repository.Verify(x => x.Get(
                    dbLog => dbLog.Id == userId,
                    null,
                    It.IsAny<Func<IQueryable<User>, IIncludableQueryable<User, object>>>(),
                    true,
                    CancellationToken.None)
            , Times.Once);

            repository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Create_AddUserToDb_InsertsUser()
        {
            var user = new User()
            {
                Id = Guid.Parse("47b176a6-535f-447b-9f09-86465f07967a"),
                Username = "unittest1",
                Password = "unittest1",
                Email = "unittest1@test.com"
            };

            // ARRANGE
            var repository = new Mock<IRepository<User>>();
            repository.Setup(x =>
                x.Insert(
                    It.IsAny<User>(),
                    CancellationToken.None
            )).ReturnsAsync(user.Id);

            var logger = new NullLogger<UserService>();

            var service = new UserService(repository: repository.Object, logger: logger);

            // ACT
            var result = await service.Create(user, CancellationToken.None);

            // ASSERT
            Assert.IsType<User>(result);
            Assert.Equal(user.Id, result.Id);
            repository.Verify(x => x.Insert(
                    It.IsAny<User>(),
                    CancellationToken.None)
            , Times.Once);

            repository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Create_AddUserToDb_ThrowsCreateException()
        {
            var user = new User()
            {
                Id = Guid.Parse("47b176a6-535f-447b-9f09-86465f07967a"),
                Username = "unittest1",
                Password = "unittest1",
                Email = "unittest1@test.com"
            };

            var exception = new CreateException();

            // ARRANGE
            var repository = new Mock<IRepository<User>>();
            repository.Setup(x =>
                x.Insert(
                    It.IsAny<User>(),
                    CancellationToken.None
            )).ThrowsAsync(exception);

            var logger = new NullLogger<UserService>();

            var service = new UserService(repository: repository.Object, logger: logger);

            // ACT/ASSERT
            await Assert.ThrowsAsync<CreateException>(() => service.Create(user, CancellationToken.None));

            repository.Verify(x => x.Insert(
                    It.IsAny<User>(),
                    CancellationToken.None)
            , Times.Once);

            repository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Create_AddUserToDb_ThrowsValidationException()
        {
            var user = new User()
            {
                Id = Guid.Parse("47b176a6-535f-447b-9f09-86465f07967a"),
                Username = "unittest1",
            };

            var exception = new ValidationException("log");

            // ARRANGE
            var repository = new Mock<IRepository<User>>();
            repository.Setup(x =>
                x.Insert(
                    It.IsAny<User>(),
                    CancellationToken.None
            )).ThrowsAsync(exception);

            var logger = new NullLogger<UserService>();

            var service = new UserService(repository: repository.Object, logger: logger);

            // ACT/ASSERT
            await Assert.ThrowsAsync<ValidationException>(async () => await service.Create(user, CancellationToken.None));

            repository.Verify(x => x.Insert(
                    It.IsAny<User>(),
                    CancellationToken.None)
            , Times.Never);

            repository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Delete_DeleteUserFromDb_DeleteUser()
        {
            var userId = new Guid("f86b3510-3466-47a1-9a6d-8325b8b305c5");
            var inputGuidToAssert = Guid.Empty;

            // ARRANGE 
            var repository = new Mock<IRepository<User>>();
            var setup = repository.Setup(x =>
                x.Delete(
                    It.IsAny<Guid>(),
                    CancellationToken.None));

            setup.Callback((Guid guid, CancellationToken token) => inputGuidToAssert = guid);

            var logger = new NullLogger<UserService>();

            var service = new UserService(repository.Object, logger);

            // ACT
            await service.Delete(userId, CancellationToken.None);

            // ASSERT
            Assert.Equal(userId, inputGuidToAssert);
            repository.Verify(x => x.Delete(
                    It.IsAny<Guid>(),
                    CancellationToken.None)
            , Times.Once);

            repository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Delete_DeleteUserFromDb_ThrowsDeleteException()
        {
            var userId = new Guid("f86b3510-3466-47a1-9a6d-8325b8b305c5");
            var inputGuidToAssert = Guid.Empty;
            var exception = new DeleteException(userId);

            // ARRANGE 
            var repository = new Mock<IRepository<User>>();
            repository.Setup(x =>
                x.Delete(
                    It.IsAny<Guid>(),
                    CancellationToken.None)).Throws(exception);

            var logger = new NullLogger<UserService>();

            var service = new UserService(repository.Object, logger);

            // ACT/ASSERT
            await Assert.ThrowsAsync<DeleteException>(async () => await service.Delete(userId, CancellationToken.None));

            repository.Verify(x => x.Delete(
                    It.IsAny<Guid>(),
                    CancellationToken.None)
            , Times.Once);

            repository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Update_UserInDb_UpdatesUser()
        {
            var user = new User()
            {
                Id = Guid.Parse("47b176a6-535f-447b-9f09-86465f07967a"),
                Username = "unittest1",
                Password = "unittest1",
                Email = "unittest1@test.com"
            };

            // ARRANGE
            var repository = new Mock<IRepository<User>>();
            var setup = repository.Setup(x =>
                x.Update(
                    It.IsAny<User>(),
                    CancellationToken.None
            ));

            var logger = new NullLogger<UserService>();

            var service = new UserService(repository: repository.Object, logger: logger);

            // ACT
            var result = await service.Update(user, CancellationToken.None);

            // ASSERT
            Assert.IsType<User>(result);
            Assert.Equal(user, result);
            repository.Verify(x => x.Update(
                    It.IsAny<User>(),
                    CancellationToken.None)
            , Times.Once);

            repository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Update_UpdateUserInDb_ThrowsUpdateException()
        {
            var user = new User()
            {
                Id = Guid.Parse("47b176a6-535f-447b-9f09-86465f07967a"),
                Username = "unittest1",
                Password = "unittest1",
                Email = "unittest1@test.com"
            };

            var exception = new UpdateException(user.Id);

            // ARRANGE
            var repository = new Mock<IRepository<User>>();
            repository.Setup(x =>
                x.Update(
                    It.IsAny<User>(),
                    CancellationToken.None
            )).Throws(exception);

            var logger = new NullLogger<UserService>();

            var service = new UserService(repository: repository.Object, logger: logger);

            // ACT/ASSERT
            await Assert.ThrowsAsync<UpdateException>(async () => await service.Update(user, CancellationToken.None));

            repository.Verify(x => x.Update(
                    It.IsAny<User>(),
                    CancellationToken.None)
            , Times.Once);

            repository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Update_UpdateExerciseInDb_ThrowsValidationException()
        {
            var user = new User()
            {
                Id = Guid.Parse("47b176a6-535f-447b-9f09-86465f07967a"),
                Username = "unittest1"
            };

            var exception = new ValidationException("log");

            // ARRANGE
            var repository = new Mock<IRepository<User>>();
            repository.Setup(x =>
                x.Update(
                    It.IsAny<User>(),
                    CancellationToken.None
            )).ThrowsAsync(exception);

            var logger = new NullLogger<UserService>();

            var service = new UserService(repository: repository.Object, logger: logger);

            // ACT/ASSERT
            await Assert.ThrowsAsync<ValidationException>(() => service.Update(user, CancellationToken.None));

            repository.Verify(x => x.Update(
                    It.IsAny<User>(),
                    CancellationToken.None)
            , Times.Never);

            repository.VerifyNoOtherCalls();
        }
    }
}
