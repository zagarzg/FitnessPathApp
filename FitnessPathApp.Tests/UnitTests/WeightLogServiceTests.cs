﻿using FitnessPathApp.BusinessLayer.Exceptions;
using FitnessPathApp.BusinessLayer.Implementations;
using FitnessPathApp.DomainLayer.Entities;
using FitnessPathApp.PersistanceLayer.Interfaces;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace FitnessPathApp.Tests.UnitTests
{
    public class WeightLogServiceTests
    {
        [Fact]
        public async Task GetAll_TwoWeightLogsInDb_GetsBoth()
        {
            var logs = new List<WeightLog>()
            {
                new WeightLog()
                {
                    Id = Guid.Parse("47b176a6-535f-447b-9f09-86465f07967a"),
                    Date = new DateTime(2022 ,11, 4)
                },
                new WeightLog()
                {
                    Id = Guid.Parse("d386146c-06ac-4325-82fa-11721ecb1d4b"),
                    Date = new DateTime(2022 ,12, 4)
                },
            };

            // ARRANGE
            var repository = new Mock<IRepository<WeightLog>>();
            repository.Setup(x =>
                x.GetAll(
                    null,
                    null,
                    It.IsAny<Func<IQueryable<WeightLog>, IIncludableQueryable<WeightLog, object>>>(),
                    true,
                    CancellationToken.None
            )).ReturnsAsync(logs);

            var logger = new NullLogger<WeightLogService>();

            var service = new WeightLogService(repository: repository.Object, logger: logger);

            // ACT
            var result = (await service.GetAll(CancellationToken.None)).ToList();

            // ASSERT
            Assert.IsType<List<WeightLog>>(result);
            Assert.Equal(logs[0].Id, result[0].Id);
            Assert.Equal(logs[1].Id, result[1].Id);

            repository.Verify(x => x.GetAll(null,
                    null,
                    It.IsAny<Func<IQueryable<WeightLog>, IIncludableQueryable<WeightLog, object>>>(),
                    true,
                    CancellationToken.None)
            , Times.Once);

            repository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Get_TwoWeightLogsInDb_GetsOne()
        {
            var logs = new List<WeightLog>()
            {
                new WeightLog()
                {
                    Id = Guid.Parse("47b176a6-535f-447b-9f09-86465f07967a"),
                    Date = new DateTime(2022 ,11, 4)
                },
                new WeightLog()
                {
                    Id = Guid.Parse("d386146c-06ac-4325-82fa-11721ecb1d4b"),
                    Date = new DateTime(2022 ,12, 4)
                },
            };

            // ARRANGE
            var repository = new Mock<IRepository<WeightLog>>();
            var id = logs[1].Id;
            repository.Setup(x =>
                x.Get(
                    dbLog => dbLog.Id == id,
                    null,
                    It.IsAny<Func<IQueryable<WeightLog>, IIncludableQueryable<WeightLog, object>>>(),
                    true,
                    CancellationToken.None
            )).ReturnsAsync(logs[1]);

            var logger = new NullLogger<WeightLogService>();

            var service = new WeightLogService(repository: repository.Object, logger: logger);

            // ACT
            var result = await service.Get(id, CancellationToken.None);

            // ASSERT
            Assert.IsType<WeightLog>(result);
            Assert.Equal(logs[1], result);

            repository.Verify(x => x.Get(
                    dbLog => dbLog.Id == id,
                    null,
                    It.IsAny<Func<IQueryable<WeightLog>, IIncludableQueryable<WeightLog, object>>>(),
                    true,
                    CancellationToken.None)
            , Times.Once);

            repository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Get_TwoWeightLogsInDb_ThrowsNotFoundException()
        {
            var logs = new List<WeightLog>()
            {
                new WeightLog()
                {
                    Id = Guid.Parse("47b176a6-535f-447b-9f09-86465f07967a"),
                    Date = new DateTime(2022 ,11, 4)
                },
                new WeightLog()
                {
                    Id = Guid.Parse("d386146c-06ac-4325-82fa-11721ecb1d4b"),
                    Date = new DateTime(2022 ,12, 4)
                },
            };

            // ARRANGE
            var repository = new Mock<IRepository<WeightLog>>();
            var logId = Guid.Parse("689f9a14-de94-424e-945f-54c92518e2e6");

            var exception = new NotFoundException(logId);

            repository.Setup(x =>
                x.Get(
                    dbLog => dbLog.Id == logId,
                    null,
                    It.IsAny<Func<IQueryable<WeightLog>, IIncludableQueryable<WeightLog, object>>>(),
                    true,
                    CancellationToken.None
            )).ReturnsAsync((WeightLog)null); 

            var logger = new NullLogger<WeightLogService>();

            var service = new WeightLogService(repository: repository.Object, logger: logger);

            // ACT/ASSERT
            await Assert.ThrowsAsync<NotFoundException>(() => service.Get(logId, CancellationToken.None));

            repository.Verify(x => x.Get(
                    dbLog => dbLog.Id == logId,
                    null,
                    It.IsAny<Func<IQueryable<WeightLog>, IIncludableQueryable<WeightLog, object>>>(),
                    true,
                    CancellationToken.None)
            , Times.Once);

            repository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Create_AddLogToDb_InsertsLog()
        {
            var log = new WeightLog()
            {
                Id = Guid.Parse("827e957c-60a0-4136-aebe-d5df4bf87c2d"),
                Date = new DateTime(2022, 1, 10),
                Value = 75,
                UserId = Guid.Parse("f87c1071-684b-4c02-81b6-298f9dbb9d96")
            };

            // ARRANGE
            var repository = new Mock<IRepository<WeightLog>>();
            repository.Setup(x =>
                x.Insert(
                    It.IsAny<WeightLog>(),
                    CancellationToken.None
            )).ReturnsAsync(log.Id);

            var logger = new NullLogger<WeightLogService>();

            var service = new WeightLogService(repository: repository.Object, logger: logger);

            // ACT
            var result = await service.Create(log, CancellationToken.None);

            // ASSERT
            Assert.IsType<WeightLog>(result);
            Assert.Equal(log.Id, result.Id);
            repository.Verify(x => x.Insert(
                    It.IsAny<WeightLog>(),
                    CancellationToken.None)
            , Times.Once);

            repository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Create_AddLogToDb_ThrowsCreateException()
        {
            var log = new WeightLog()
            {
                Id = Guid.Parse("fbe588e5-7741-4e7c-bf29-b3caab76723e"),
                Date = new DateTime(2022, 1, 10),
                Value = 78,
                UserId = Guid.Parse("35c75c75-2309-4258-8241-a857f25602d8")
            };

            var exception = new CreateException();

            // ARRANGE
            var repository = new Mock<IRepository<WeightLog>>();
            repository.Setup(x =>
                x.Insert(
                    It.IsAny<WeightLog>(),
                    CancellationToken.None
            )).ThrowsAsync(exception);

            var logger = new NullLogger<WeightLogService>();

            var service = new WeightLogService(repository: repository.Object, logger: logger);

            // ACT/ASSERT
            await Assert.ThrowsAsync<CreateException>(() => service.Create(log, CancellationToken.None));

            repository.Verify(x => x.Insert(
                    It.IsAny<WeightLog>(),
                    CancellationToken.None)
            , Times.Once);

            repository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Create_AddLogToDb_ThrowsValidationException()
        {
            var log = new WeightLog()
            {
                Id = Guid.Parse("38e3f113-bf7f-4bbc-86d5-d9d502a411f5"),
                Date = new DateTime(2022, 1, 10),
                Value = -2,
                UserId = Guid.Parse("35c75c75-2309-4258-8241-a857f25602d8")
            };

            var exception = new ValidationException("log");

            // ARRANGE
            var repository = new Mock<IRepository<WeightLog>>();
            repository.Setup(x =>
                x.Insert(
                    It.IsAny<WeightLog>(),
                    CancellationToken.None
            )).ThrowsAsync(exception);

            var logger = new NullLogger<WeightLogService>();

            var service = new WeightLogService(repository: repository.Object, logger: logger);

            // ACT/ASSERT
            await Assert.ThrowsAsync<ValidationException>(async () => await service.Create(log, CancellationToken.None));

            repository.Verify(x => x.Insert(
                    It.IsAny<WeightLog>(),
                    CancellationToken.None)
            , Times.Never);

            repository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Delete_DeleteLogFromDb_DeleteLog()
        {
            var logId = new Guid("f86b3510-3466-47a1-9a6d-8325b8b305c5");
            var inputGuidToAssert = Guid.Empty;

            // ARRANGE 
            var repository = new Mock<IRepository<WeightLog>>();
            var setup = repository.Setup(x =>
                x.Delete(
                    It.IsAny<Guid>(),
                    CancellationToken.None));

            setup.Callback((Guid guid, CancellationToken token) => inputGuidToAssert = guid);

            var logger = new NullLogger<WeightLogService>();

            var service = new WeightLogService(repository.Object, logger);

            // ACT
            await service.Delete(logId, CancellationToken.None);

            // ASSERT
            Assert.Equal(logId, inputGuidToAssert);
            repository.Verify(x => x.Delete(
                    It.IsAny<Guid>(),
                    CancellationToken.None)
            , Times.Once);

            repository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Delete_DeleteLogFromDb_ThrowsDeleteException()
        {
            var logId = new Guid("f86b3510-3466-47a1-9a6d-8325b8b305c5");
            var inputGuidToAssert = Guid.Empty;
            var exception = new DeleteException(logId);

            // ARRANGE 
            var repository = new Mock<IRepository<WeightLog>>();
            repository.Setup(x =>
                x.Delete(
                    It.IsAny<Guid>(),
                    CancellationToken.None)).Throws(exception);

            var logger = new NullLogger<WeightLogService>();

            var service = new WeightLogService(repository.Object, logger);

            // ACT/ASSERT
            await Assert.ThrowsAsync<DeleteException>(async () => await service.Delete(logId, CancellationToken.None));

            repository.Verify(x => x.Delete(
                    It.IsAny<Guid>(),
                    CancellationToken.None)
            , Times.Once);

            repository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Update_UpdateLogInDb_UpdatesLog()
        {
            var log = new WeightLog()
            {
                Id = Guid.Parse("827e957c-60a0-4136-aebe-d5df4bf87c2d"),
                Date = new DateTime(2022, 1, 10),
                Value = 75,
                UserId = Guid.Parse("f87c1071-684b-4c02-81b6-298f9dbb9d96")
            };

            // ARRANGE
            var repository = new Mock<IRepository<WeightLog>>();
            var setup = repository.Setup(x =>
                x.Update(
                    It.IsAny<WeightLog>(),
                    CancellationToken.None
            ));

            var logger = new NullLogger<WeightLogService>();

            var service = new WeightLogService(repository: repository.Object, logger: logger);

            // ACT
            var result = await service.Update(log, CancellationToken.None);

            // ASSERT
            Assert.IsType<WeightLog>(result);
            Assert.Equal(log, result);
            repository.Verify(x => x.Update(
                    It.IsAny<WeightLog>(),
                    CancellationToken.None)
            , Times.Once);

            repository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Update_UpdateLogInDb_ThrowsUpdateException()
        {
            var log = new WeightLog()
            {
                Id = Guid.Parse("827e957c-60a0-4136-aebe-d5df4bf87c2d"),
                Date = new DateTime(2022, 1, 10),
                Value = 75,
                UserId = Guid.Parse("f87c1071-684b-4c02-81b6-298f9dbb9d96")
            };

            var exception = new UpdateException(log.Id);

            // ARRANGE
            var repository = new Mock<IRepository<WeightLog>>();
            repository.Setup(x =>
                x.Update(
                    It.IsAny<WeightLog>(),
                    CancellationToken.None
            )).Throws(exception);

            var logger = new NullLogger<WeightLogService>();

            var service = new WeightLogService(repository: repository.Object, logger: logger);

            // ACT/ASSERT
            await Assert.ThrowsAsync<UpdateException>(async () => await service.Update(log, CancellationToken.None));

            repository.Verify(x => x.Update(
                    It.IsAny<WeightLog>(),
                    CancellationToken.None)
            , Times.Once);

            repository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Update_UpdateLogInDb_ThrowsValidationException()
        {
            var log = new WeightLog()
            {
                Id = Guid.Parse("827e957c-60a0-4136-aebe-d5df4bf87c2d"),
                Date = new DateTime(2022, 1, 10),
                Value = -2,
                UserId = Guid.Parse("f87c1071-684b-4c02-81b6-298f9dbb9d96")
            };

            var exception = new ValidationException("log");

            // ARRANGE
            var repository = new Mock<IRepository<WeightLog>>();
            repository.Setup(x =>
                x.Update(
                    It.IsAny<WeightLog>(),
                    CancellationToken.None
            )).ThrowsAsync(exception);

            var logger = new NullLogger<WeightLogService>();

            var service = new WeightLogService(repository: repository.Object, logger: logger);

            // ACT/ASSERT
            await Assert.ThrowsAsync<ValidationException>(() => service.Update(log, CancellationToken.None));

            repository.Verify(x => x.Update(
                    It.IsAny<WeightLog>(),
                    CancellationToken.None)
            , Times.Never);

            repository.VerifyNoOtherCalls();
        }
    }
}
