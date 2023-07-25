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
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace FitnessPathApp.Tests.UnitTests
{
    public class ExerciseChoiceServiceTests
    {
        private static Mapper _mapper;

        public ExerciseChoiceServiceTests()
        {
            var mapperConfig = new MapperConfiguration(cfg => {
                cfg.CreateMap<ExerciseChoice, ExerciseChoiceDTO>();
            });
            _mapper = new Mapper(mapperConfig);
        }

        [Fact]
        public async Task GetAll_TwoExerciseChoicesInDb_GetsBoth()
        {
            var exerciseChoices = new List<ExerciseChoice>()
            {
                new ExerciseChoice()
                {
                    Id = Guid.Parse("47b176a6-535f-447b-9f09-86465f07967a"),
                    Name = "Bench Press",
                    ExerciseType = ExerciseType.Compound,
                    ImageUrl = "Bench Press Image",
                    IsFavorite = true,
                    Exercises = new List<Exercise>()
                },
                new ExerciseChoice()
                {
                    Id = Guid.Parse("d386146c-06ac-4325-82fa-11721ecb1d4b"),
                    Name = "Squat",
                    ExerciseType = ExerciseType.Compound,
                    ImageUrl = "Squat Image",
                    IsFavorite = true,
                    Exercises = new List<Exercise>()
                },
            };

            // ARRANGE
            var repository = new Mock<IRepository<ExerciseChoice>>();
            repository.Setup(x =>
                x.GetAll(
                    null,
                    null,
                    It.IsAny<Func<IQueryable<ExerciseChoice>, IIncludableQueryable<ExerciseChoice, object>>>(),
                    true,
                    CancellationToken.None
            )).ReturnsAsync(exerciseChoices);

            var logger = new NullLogger<ExerciseChoiceService>();

            var service = new ExerciseChoiceService(repository: repository.Object, logger: logger, mapper: _mapper);

            // ACT
            var result = (await service.GetAll(CancellationToken.None)).ToList();

            // ASSERT
            Assert.IsType<List<ExerciseChoiceDTO>>(result);
            Assert.Equal(exerciseChoices[0].Id, result[0].Id);
            Assert.Equal(exerciseChoices[1].Id, result[1].Id);

            repository.Verify(x => x.GetAll(null,
                    null,
                    It.IsAny<Func<IQueryable<ExerciseChoice>, IIncludableQueryable<ExerciseChoice, object>>>(),
                    true,
                    CancellationToken.None)
            , Times.Once);

            repository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Get_TwoExerciseChoicesInDb_GetsOne()
        {
            var exerciseChoices = new List<ExerciseChoice>()
            {
                new ExerciseChoice()
                {
                    Id = Guid.Parse("47b176a6-535f-447b-9f09-86465f07967a"),
                    Name = "Bench Press",
                    ExerciseType = ExerciseType.Compound,
                    ImageUrl = "Bench Press Image",
                    IsFavorite = true,
                    Exercises = new List<Exercise>()
                },
                new ExerciseChoice()
                {
                    Id = Guid.Parse("d386146c-06ac-4325-82fa-11721ecb1d4b"),
                    Name = "Squat",
                    ExerciseType = ExerciseType.Compound,
                    ImageUrl = "Squat Image",
                    IsFavorite = true,
                    Exercises = new List<Exercise>()
                },
            };

            // ARRANGE
            var repository = new Mock<IRepository<ExerciseChoice>>();
            var id = exerciseChoices[1].Id;
            repository.Setup(x =>
                x.Get(
                    dbLog => dbLog.Id == id,
                    null,
                    It.IsAny<Func<IQueryable<ExerciseChoice>, IIncludableQueryable<ExerciseChoice, object>>>(),
                    true,
                    CancellationToken.None
            )).ReturnsAsync(exerciseChoices[1]);

            var logger = new NullLogger<ExerciseChoiceService>();

            var service = new ExerciseChoiceService(repository: repository.Object, logger: logger, mapper: _mapper);

            // ACT
            var result = await service.Get(id, CancellationToken.None);

            // ASSERT
            Assert.IsType<ExerciseChoiceDTO>(result);
            Assert.Equal(exerciseChoices[1].Id, result.Id);

            repository.Verify(x => x.Get(
                    dbLog => dbLog.Id == id,
                    null,
                    It.IsAny<Func<IQueryable<ExerciseChoice>, IIncludableQueryable<ExerciseChoice, object>>>(),
                    true,
                    CancellationToken.None)
            , Times.Once);

            repository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Get_TwoExerciseChoicesInDb_ThrowsNotFoundException()
        {
            var exerciseChoices = new List<ExerciseChoice>()
            {
                new ExerciseChoice()
                {
                    Id = Guid.Parse("47b176a6-535f-447b-9f09-86465f07967a"),
                    Name = "Bench Press",
                    ExerciseType = ExerciseType.Compound,
                    ImageUrl = "Bench Press Image",
                    IsFavorite = true,
                    Exercises = new List<Exercise>()
                },
                new ExerciseChoice()
                {
                    Id = Guid.Parse("d386146c-06ac-4325-82fa-11721ecb1d4b"),
                    Name = "Squat",
                    ExerciseType = ExerciseType.Compound,
                    ImageUrl = "Squat Image",
                    IsFavorite = true,
                    Exercises = new List<Exercise>()
                },
            };

            // ARRANGE
            var repository = new Mock<IRepository<ExerciseChoice>>();
            var exerciseChoiceId = Guid.Parse("689f9a14-de94-424e-945f-54c92518e2e6");

            var exception = new NotFoundException(exerciseChoiceId);

            repository.Setup(x =>
                x.Get(
                    dbExercise => dbExercise.Id == exerciseChoiceId,
                    null,
                    It.IsAny<Func<IQueryable<ExerciseChoice>, IIncludableQueryable<ExerciseChoice, object>>>(),
                    true,
                    CancellationToken.None
            )).ReturnsAsync((ExerciseChoice)null);

            var logger = new NullLogger<ExerciseChoiceService>();

            var service = new ExerciseChoiceService(repository: repository.Object, logger: logger, mapper: _mapper);

            // ACT/ASSERT
            await Assert.ThrowsAsync<NotFoundException>(() => service.Get(exerciseChoiceId, CancellationToken.None));

            repository.Verify(x => x.Get(
                    dbLog => dbLog.Id == exerciseChoiceId,
                    null,
                    It.IsAny<Func<IQueryable<ExerciseChoice>, IIncludableQueryable<ExerciseChoice, object>>>(),
                    true,
                    CancellationToken.None)
            , Times.Once);

            repository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Create_AddExerciseChoiceToDb_InsertsExerciseChoice()
        {
            var exerciseChoice = new ExerciseChoice()
            {
                Id = Guid.Parse("0ee5c293-b77d-442f-a13a-5e36ce4d12ef"),
                Name = "Deadlift",
                ExerciseType = ExerciseType.Compound,
                ImageUrl = "Deadlift Image",
                IsFavorite = true,
                Exercises = new List<Exercise>()
            };

            // ARRANGE
            var repository = new Mock<IRepository<ExerciseChoice>>();
            repository.Setup(x =>
                x.Insert(
                    It.IsAny<ExerciseChoice>(),
                    CancellationToken.None
            )).ReturnsAsync(exerciseChoice.Id);

            var logger = new NullLogger<ExerciseChoiceService>();

            var service = new ExerciseChoiceService(repository: repository.Object, logger: logger, mapper: _mapper);

            // ACT
            var result = await service.Create(exerciseChoice, CancellationToken.None);
            Console.WriteLine(result);

            // ASSERT
            Assert.IsType<ExerciseChoiceDTO>(result);
            Assert.Equal(exerciseChoice.Id, result.Id);
            repository.Verify(x => x.Insert(
                    It.IsAny<ExerciseChoice>(),
                    CancellationToken.None)
            , Times.Once);

            repository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Create_AddExerciseChoiceToDb_ThrowsCreateException()
        {
            var exerciseChoice = new ExerciseChoice()
            {
                Id = Guid.Parse("0ee5c293-b77d-442f-a13a-5e36ce4d12ef"),
                Name = "Deadlift",
                ExerciseType = ExerciseType.Compound,
                ImageUrl = "Deadlift Image",
                IsFavorite = true,
                Exercises = new List<Exercise>()
            };

            var exception = new CreateException();

            // ARRANGE
            var repository = new Mock<IRepository<ExerciseChoice>>();
            repository.Setup(x =>
                x.Insert(
                    It.IsAny<ExerciseChoice>(),
                    CancellationToken.None
            )).ThrowsAsync(exception);

            var logger = new NullLogger<ExerciseChoiceService>();

            var service = new ExerciseChoiceService(repository: repository.Object, logger: logger, mapper: _mapper);

            // ACT/ASSERT
            await Assert.ThrowsAsync<CreateException>(() => service.Create(exerciseChoice, CancellationToken.None));

            repository.Verify(x => x.Insert(
                    It.IsAny<ExerciseChoice>(),
                    CancellationToken.None)
            , Times.Once);

            repository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Create_AddExerciseChoiceToDb_ThrowsValidationException()
        {
            var exerciseChoice = new ExerciseChoice()
            {
                Id = Guid.Parse("0ee5c293-b77d-442f-a13a-5e36ce4d12ef"),
                Name = "Deadlift",
                ImageUrl = "Deadlift Image",
                IsFavorite = true,
                Exercises = new List<Exercise>()
            };

            var exception = new ValidationException("log");

            // ARRANGE
            var repository = new Mock<IRepository<ExerciseChoice>>();
            repository.Setup(x =>
                x.Insert(
                    It.IsAny<ExerciseChoice>(),
                    CancellationToken.None
            )).ThrowsAsync(exception);

            var logger = new NullLogger<ExerciseChoiceService>();

            var service = new ExerciseChoiceService(repository: repository.Object, logger: logger, mapper: _mapper);

            // ACT/ASSERT
            await Assert.ThrowsAsync<ValidationException>(async () => await service.Create(exerciseChoice, CancellationToken.None));

            repository.Verify(x => x.Insert(
                    It.IsAny<ExerciseChoice>(),
                    CancellationToken.None)
            , Times.Never);

            repository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Delete_DeleteExerciseChoiceFromDb_DeleteExerciseChoice()
        {
            var exerciseChoiceId = new Guid("f86b3510-3466-47a1-9a6d-8325b8b305c5");
            var inputGuidToAssert = Guid.Empty;

            // ARRANGE 
            var repository = new Mock<IRepository<ExerciseChoice>>();
            var setup = repository.Setup(x =>
                x.Delete(
                    It.IsAny<Guid>(),
                    CancellationToken.None));

            setup.Callback((Guid guid, CancellationToken token) => inputGuidToAssert = guid);

            var logger = new NullLogger<ExerciseChoiceService>();

            var service = new ExerciseChoiceService(repository.Object, logger, mapper: _mapper);

            // ACT
            await service.Delete(exerciseChoiceId, CancellationToken.None);

            // ASSERT
            Assert.Equal(exerciseChoiceId, inputGuidToAssert);
            repository.Verify(x => x.Delete(
                    It.IsAny<Guid>(),
                    CancellationToken.None)
            , Times.Once);

            repository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Delete_DeleteExerciseChoiceFromDb_ThrowsDeleteException()
        {
            var exerciseChoiceId = new Guid("f86b3510-3466-47a1-9a6d-8325b8b305c5");
            var inputGuidToAssert = Guid.Empty;
            var exception = new DeleteException(exerciseChoiceId);

            // ARRANGE 
            var repository = new Mock<IRepository<ExerciseChoice>>();
            repository.Setup(x =>
                x.Delete(
                    It.IsAny<Guid>(),
                    CancellationToken.None)).Throws(exception);

            var logger = new NullLogger<ExerciseChoiceService>();

            var service = new ExerciseChoiceService(repository.Object, logger, mapper: _mapper);

            // ACT/ASSERT
            await Assert.ThrowsAsync<DeleteException>(async () => await service.Delete(exerciseChoiceId, CancellationToken.None));

            repository.Verify(x => x.Delete(
                    It.IsAny<Guid>(),
                    CancellationToken.None)
            , Times.Once);

            repository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Update_ExerciseChoiceInDb_UpdatesExerciseChoice()
        {
            var exerciseChoice = new ExerciseChoice()
            {
                Id = Guid.Parse("0ee5c293-b77d-442f-a13a-5e36ce4d12ef"),
                Name = "Deadlift",
                ExerciseType = ExerciseType.Compound,
                ImageUrl = "Deadlift Image",
                IsFavorite = true,
                Exercises = new List<Exercise>()
            };

            // ARRANGE
            var repository = new Mock<IRepository<ExerciseChoice>>();
            var setup = repository.Setup(x =>
                x.Update(
                    It.IsAny<ExerciseChoice>(),
                    CancellationToken.None
            ));

            var logger = new NullLogger<ExerciseChoiceService>();

            var service = new ExerciseChoiceService(repository: repository.Object, logger: logger, mapper: _mapper);

            // ACT
            var result = await service.Update(exerciseChoice, CancellationToken.None);

            // ASSERT
            Assert.IsType<ExerciseChoiceDTO>(result);
            Assert.Equal(exerciseChoice.Id, result.Id);
            repository.Verify(x => x.Update(
                    It.IsAny<ExerciseChoice>(),
                    CancellationToken.None)
            , Times.Once);

            repository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Update_UpdateExerciseChoiceInDb_ThrowsUpdateException()
        {
            var exerciseChoice = new ExerciseChoice()
            {
                Id = Guid.Parse("0ee5c293-b77d-442f-a13a-5e36ce4d12ef"),
                Name = "Deadlift",
                ExerciseType = ExerciseType.Compound,
                ImageUrl = "Deadlift Image",
                IsFavorite = true,
                Exercises = new List<Exercise>()
            };

            var exception = new UpdateException(exerciseChoice.Id);

            // ARRANGE
            var repository = new Mock<IRepository<ExerciseChoice>>();
            repository.Setup(x =>
                x.Update(
                    It.IsAny<ExerciseChoice>(),
                    CancellationToken.None
            )).Throws(exception);

            var logger = new NullLogger<ExerciseChoiceService>();

            var service = new ExerciseChoiceService(repository: repository.Object, logger: logger, mapper: _mapper);

            // ACT/ASSERT
            await Assert.ThrowsAsync<UpdateException>(async () => await service.Update(exerciseChoice, CancellationToken.None));

            repository.Verify(x => x.Update(
                    It.IsAny<ExerciseChoice>(),
                    CancellationToken.None)
            , Times.Once);

            repository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Update_UpdateExerciseInDb_ThrowsValidationException()
        {
            var exerciseChoice = new ExerciseChoice()
            {
                Id = Guid.Parse("0ee5c293-b77d-442f-a13a-5e36ce4d12ef"),
                Name = "Deadlift",
                ImageUrl = "Deadlift Image",
                IsFavorite = true,
                Exercises = new List<Exercise>()
            };

            var exception = new ValidationException("log");

            // ARRANGE
            var repository = new Mock<IRepository<ExerciseChoice>>();
            repository.Setup(x =>
                x.Update(
                    It.IsAny<ExerciseChoice>(),
                    CancellationToken.None
            )).ThrowsAsync(exception);

            var logger = new NullLogger<ExerciseChoiceService>();

            var service = new ExerciseChoiceService(repository: repository.Object, logger: logger, mapper: _mapper);

            // ACT/ASSERT
            await Assert.ThrowsAsync<ValidationException>(() => service.Update(exerciseChoice, CancellationToken.None));

            repository.Verify(x => x.Update(
                    It.IsAny<ExerciseChoice>(),
                    CancellationToken.None)
            , Times.Never);

            repository.VerifyNoOtherCalls();
        }
      
    }
}
