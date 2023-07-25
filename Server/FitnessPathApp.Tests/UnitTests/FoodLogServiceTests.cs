using AutoMapper;
using FitnessPathApp.BusinessLayer.Exceptions;
using FitnessPathApp.BusinessLayer.Implementations;
using FitnessPathApp.DomainLayer.Entities;
using FitnessPathApp.PersistanceLayer.DTOs;
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
    public class FoodLogServiceTests
    {
        private static Mapper _mapper;

        public FoodLogServiceTests()
        {
            var mapperConfig = new MapperConfiguration(cfg => {
                cfg.CreateMap<FoodLog, FoodLogDTO>();
                cfg.CreateMap<FoodItem, FoodItemDTO>();
            });
            _mapper = new Mapper(mapperConfig);
        }
        [Fact]
        public async Task GetAll_TwoFoodLogsInDb_GetsBoth()
        {
            var logs = new List<FoodLog>()
            {
                new FoodLog()
                {
                    Id = Guid.Parse("47b176a6-535f-447b-9f09-86465f07967a"),
                    Date = new DateTime(2022 ,11, 4),
                    FoodItems = new List<FoodItem>
                    {
                        new FoodItem()
                        {
                            Id = Guid.Parse("47b176a6-535f-447b-9f09-86465f07967a"),
                            Name = "Chicken",
                            Calories = 300,
                            Carbs = 10,
                            Protein = 22,
                            Fat = 5,
                            FoodLogId = Guid.Parse("47b176a6-535f-447b-9f09-86465f07967a"),
                        },
                        new FoodItem()
                        {
                            Id = Guid.Parse("d386146c-06ac-4325-82fa-11721ecb1d4b"),
                            Name = "Rice",
                            Calories = 300,
                            Carbs = 80,
                            Protein = 20,
                            Fat = 2,
                            FoodLogId = Guid.Parse("47b176a6-535f-447b-9f09-86465f07967a"),
                        },
                    }
                },
                new FoodLog()
                {
                    Id = Guid.Parse("d386146c-06ac-4325-82fa-11721ecb1d4b"),
                    Date = new DateTime(2022 ,12, 4)
                },
            };

            // ARRANGE
            var repository = new Mock<IRepository<FoodLog>>();
            repository.Setup(x =>
                x.GetAll(
                    null,
                    null,
                    It.IsAny<Func<IQueryable<FoodLog>, IIncludableQueryable<FoodLog, object>>>(),
                    true,
                    CancellationToken.None
            )).ReturnsAsync(logs);

            var logger = new NullLogger<FoodLogService>();

            var service = new FoodLogService(repository: repository.Object, logger: logger, mapper: _mapper);

            // ACT
            var result = (await service.GetAll(CancellationToken.None)).ToList();

            // ASSERT
            Assert.IsType<List<FoodLogDTO>>(result);
            Assert.Equal(2, result[0].FoodItems.Count);
            Assert.Equal(logs[0].Id, result[0].Id);
            Assert.Equal(logs[1].Id, result[1].Id);

            repository.Verify(x => x.GetAll(null,
                    null,
                    It.IsAny<Func<IQueryable<FoodLog>, IIncludableQueryable<FoodLog, object>>>(),
                    true,
                    CancellationToken.None)
            , Times.Once);

            repository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Get_TwoFoodLogsInDb_GetsOne()
        {
            var logs = new List<FoodLog>()
            {
                new FoodLog()
                {
                    Id = Guid.Parse("47b176a6-535f-447b-9f09-86465f07967a"),
                    Date = new DateTime(2022 ,11, 4),
                    FoodItems = new List<FoodItem>
                    {
                        new FoodItem()
                        {
                            Id = Guid.Parse("47b176a6-535f-447b-9f09-86465f07967a"),
                            Name = "Chicken",
                            Calories = 300,
                            Carbs = 10,
                            Protein = 22,
                            Fat = 5,
                            FoodLogId = Guid.Parse("47b176a6-535f-447b-9f09-86465f07967a"),
                        },
                        new FoodItem()
                        {
                            Id = Guid.Parse("d386146c-06ac-4325-82fa-11721ecb1d4b"),
                            Name = "Rice",
                            Calories = 300,
                            Carbs = 80,
                            Protein = 20,
                            Fat = 2,
                            FoodLogId = Guid.Parse("47b176a6-535f-447b-9f09-86465f07967a"),
                        },
                    }
                },
                new FoodLog()
                {
                    Id = Guid.Parse("d386146c-06ac-4325-82fa-11721ecb1d4b"),
                    Date = new DateTime(2022 ,12, 4)
                },
            };

            // ARRANGE
            var repository = new Mock<IRepository<FoodLog>>();
            var id = logs[1].Id;
            repository.Setup(x =>
                x.Get(
                    dbLog => dbLog.Id == id,
                    null,
                    It.IsAny<Func<IQueryable<FoodLog>, IIncludableQueryable<FoodLog, object>>>(),
                    true,
                    CancellationToken.None
            )).ReturnsAsync(logs[1]);

            var logger = new NullLogger<FoodLogService>();

            var service = new FoodLogService(repository: repository.Object, logger: logger, mapper: _mapper);

            // ACT
            var result = await service.Get(id, CancellationToken.None);

            // ASSERT
            Assert.IsType<FoodLogDTO>(result);
            Assert.Equal(logs[1].Id, result.Id);

            repository.Verify(x => x.Get(
                    dbLog => dbLog.Id == id,
                    null,
                    It.IsAny<Func<IQueryable<FoodLog>, IIncludableQueryable<FoodLog, object>>>(),
                    true,
                    CancellationToken.None)
            , Times.Once);

            repository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Get_TwoFoodLogsInDb_ThrowsNotFoundException()
        {
            var logs = new List<FoodLog>()
            {
                new FoodLog()
                {
                    Id = Guid.Parse("47b176a6-535f-447b-9f09-86465f07967a"),
                    Date = new DateTime(2022 ,11, 4),
                    FoodItems = new List<FoodItem>
                    {
                        new FoodItem()
                        {
                            Id = Guid.Parse("47b176a6-535f-447b-9f09-86465f07967a"),
                            Name = "Chicken",
                            Calories = 300,
                            Carbs = 10,
                            Protein = 22,
                            Fat = 5,
                            FoodLogId = Guid.Parse("47b176a6-535f-447b-9f09-86465f07967a"),
                        },
                        new FoodItem()
                        {
                            Id = Guid.Parse("d386146c-06ac-4325-82fa-11721ecb1d4b"),
                            Name = "Rice",
                            Calories = 300,
                            Carbs = 80,
                            Protein = 20,
                            Fat = 2,
                            FoodLogId = Guid.Parse("47b176a6-535f-447b-9f09-86465f07967a"),
                        },
                    }
                },
                new FoodLog()
                {
                    Id = Guid.Parse("d386146c-06ac-4325-82fa-11721ecb1d4b"),
                    Date = new DateTime(2022 ,12, 4)
                },
            };

            // ARRANGE
            var repository = new Mock<IRepository<FoodLog>>();
            var logId = Guid.Parse("689f9a14-de94-424e-945f-54c92518e2e6");

            var exception = new NotFoundException(logId);

            repository.Setup(x =>
                x.Get(
                    dbLog => dbLog.Id == logId,
                    null,
                    It.IsAny<Func<IQueryable<FoodLog>, IIncludableQueryable<FoodLog, object>>>(),
                    true,
                    CancellationToken.None
            )).ReturnsAsync((FoodLog)null);

            var logger = new NullLogger<FoodLogService>();

            var service = new FoodLogService(repository: repository.Object, logger: logger, mapper: _mapper);

            // ACT/ASSERT
            await Assert.ThrowsAsync<NotFoundException>(() => service.Get(logId, CancellationToken.None));

            repository.Verify(x => x.Get(
                    dbLog => dbLog.Id == logId,
                    null,
                    It.IsAny<Func<IQueryable<FoodLog>, IIncludableQueryable<FoodLog, object>>>(),
                    true,
                    CancellationToken.None)
            , Times.Once);

            repository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Create_AddLogToDb_InsertsLog()
        {
            var log = new FoodLog()
            {
                Id = Guid.Parse("d386146c-06ac-4325-82fa-11721ecb1d4b"),
                Date = new DateTime(2022, 1, 4),
                UserId = Guid.Parse("f87c1071-684b-4c02-81b6-298f9dbb9d96")
            };

            // ARRANGE
            var repository = new Mock<IRepository<FoodLog>>();
            repository.Setup(x =>
                x.Insert(
                    It.IsAny<FoodLog>(),
                    CancellationToken.None
            )).ReturnsAsync(log.Id);

            var logger = new NullLogger<FoodLogService>();

            var service = new FoodLogService(repository: repository.Object, logger: logger, mapper: _mapper);

            // ACT
            var result = await service.Create(log, CancellationToken.None);

            // ASSERT
            Assert.IsType<FoodLogDTO>(result);
            Assert.Equal(log.Id, result.Id);
            repository.Verify(x => x.Insert(
                    It.IsAny<FoodLog>(),
                    CancellationToken.None)
            , Times.Once);

            repository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Create_AddLogToDb_ThrowsCreateException()
        {
            var log = new FoodLog()
            {
                Id = Guid.Parse("d386146c-06ac-4325-82fa-11721ecb1d4b"),
                Date = new DateTime(2022, 1, 4),
                UserId = Guid.Parse("f87c1071-684b-4c02-81b6-298f9dbb9d96")
            };

            var exception = new CreateException();

            // ARRANGE
            var repository = new Mock<IRepository<FoodLog>>();
            repository.Setup(x =>
                x.Insert(
                    It.IsAny<FoodLog>(),
                    CancellationToken.None
            )).ThrowsAsync(exception);

            var logger = new NullLogger<FoodLogService>();

            var service = new FoodLogService(repository: repository.Object, logger: logger, mapper: _mapper);

            // ACT/ASSERT
            await Assert.ThrowsAsync<CreateException>(() => service.Create(log, CancellationToken.None));

            repository.Verify(x => x.Insert(
                    It.IsAny<FoodLog>(),
                    CancellationToken.None)
            , Times.Once);

            repository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Create_AddLogToDb_ThrowsValidationException()
        {
            var log = new FoodLog()
            {
                Id = Guid.Parse("d386146c-06ac-4325-82fa-11721ecb1d4b"),
                Date = DateTime.Now.AddDays(7)
            };

            var exception = new ValidationException("log");

            // ARRANGE
            var repository = new Mock<IRepository<FoodLog>>();
            repository.Setup(x =>
                x.Insert(
                    It.IsAny<FoodLog>(),
                    CancellationToken.None
            )).ThrowsAsync(exception);

            var logger = new NullLogger<FoodLogService>();

            var service = new FoodLogService(repository: repository.Object, logger: logger, mapper: _mapper);

            // ACT/ASSERT
            await Assert.ThrowsAsync<ValidationException>(async () => await service.Create(log, CancellationToken.None));

            repository.Verify(x => x.Insert(
                    It.IsAny<FoodLog>(),
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
            var repository = new Mock<IRepository<FoodLog>>();
            var setup = repository.Setup(x =>
                x.Delete(
                    It.IsAny<Guid>(),
                    CancellationToken.None));

            setup.Callback((Guid guid, CancellationToken token) => inputGuidToAssert = guid);

            var logger = new NullLogger<FoodLogService>();

            var service = new FoodLogService(repository.Object, logger, mapper: _mapper);

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
            var repository = new Mock<IRepository<FoodLog>>();
            repository.Setup(x =>
                x.Delete(
                    It.IsAny<Guid>(),
                    CancellationToken.None)).Throws(exception);

            var logger = new NullLogger<FoodLogService>();

            var service = new FoodLogService(repository.Object, logger, mapper: _mapper);

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
            var log = new FoodLog()
            {
                Id = Guid.Parse("d386146c-06ac-4325-82fa-11721ecb1d4b"),
                Date = new DateTime(2022, 1, 4),
                UserId = Guid.Parse("f87c1071-684b-4c02-81b6-298f9dbb9d96")
            };

            // ARRANGE
            var repository = new Mock<IRepository<FoodLog>>();
            var setup = repository.Setup(x =>
                x.Update(
                    It.IsAny<FoodLog>(),
                    CancellationToken.None
            ));

            var logger = new NullLogger<FoodLogService>();

            var service = new FoodLogService(repository: repository.Object, logger: logger, mapper: _mapper);

            // ACT
            var result = await service.Update(log, CancellationToken.None);

            // ASSERT
            Assert.IsType<FoodLogDTO>(result);
            Assert.Equal(log.Id, result.Id);
            repository.Verify(x => x.Update(
                    It.IsAny<FoodLog>(),
                    CancellationToken.None)
            , Times.Once);

            repository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Update_UpdateLogInDb_ThrowsUpdateException()
        {
            var log = new FoodLog()
            {
                Id = Guid.Parse("d386146c-06ac-4325-82fa-11721ecb1d4b"),
                Date = DateTime.Now.AddDays(-7),
                UserId = Guid.Parse("f87c1071-684b-4c02-81b6-298f9dbb9d96")
            };

            var exception = new UpdateException(log.Id);

            // ARRANGE
            var repository = new Mock<IRepository<FoodLog>>();
            repository.Setup(x =>
                x.Update(
                    It.IsAny<FoodLog>(),
                    CancellationToken.None
            )).Throws(exception);

            var logger = new NullLogger<FoodLogService>();

            var service = new FoodLogService(repository: repository.Object, logger: logger, mapper: _mapper);

            // ACT/ASSERT
            await Assert.ThrowsAsync<UpdateException>(async () => await service.Update(log, CancellationToken.None));

            repository.Verify(x => x.Update(
                    It.IsAny<FoodLog>(),
                    CancellationToken.None)
            , Times.Once);

            repository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Update_UpdateLogInDb_ThrowsValidationException()
        {
            var log = new FoodLog()
            {
                Id = Guid.Parse("d386146c-06ac-4325-82fa-11721ecb1d4b"),
                Date = DateTime.Now.AddDays(7),
                UserId = Guid.Parse("f87c1071-684b-4c02-81b6-298f9dbb9d96")
            };

            var exception = new ValidationException("log");

            // ARRANGE
            var repository = new Mock<IRepository<FoodLog>>();
            repository.Setup(x =>
                x.Update(
                    It.IsAny<FoodLog>(),
                    CancellationToken.None
            )).ThrowsAsync(exception);

            var logger = new NullLogger<FoodLogService>();

            var service = new FoodLogService(repository: repository.Object, logger: logger, mapper: _mapper);

            // ACT/ASSERT
            await Assert.ThrowsAsync<ValidationException>(() => service.Update(log, CancellationToken.None));

            repository.Verify(x => x.Update(
                    It.IsAny<FoodLog>(),
                    CancellationToken.None)
            , Times.Never);

            repository.VerifyNoOtherCalls();
        }
    }
}
