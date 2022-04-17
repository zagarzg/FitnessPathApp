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
    public class ExerciseServiceTests
    {
        [Fact]
        public async Task GetAll_TwoExercisesInDb_GetsBoth()
        {
            var exercises = new List<Exercise>()
            {
                new Exercise()
                {
                    Id = Guid.Parse("47b176a6-535f-447b-9f09-86465f07967a"),
                    Name = "Bench Press",
                    Weight = 100,
                    Sets = 5,
                    Reps = 5,
                    TrainingLogId = Guid.Parse("cf4509d5-630c-470e-81c0-22628863c0d8"),
                },
                new Exercise()
                {
                    Id = Guid.Parse("d386146c-06ac-4325-82fa-11721ecb1d4b"),
                    Name = "Squat",
                    Weight = 140,
                    Sets = 3,
                    Reps = 5,
                    TrainingLogId = Guid.Parse("cf4509d5-630c-470e-81c0-22628863c0d8"),
                },
            };

            // ARRANGE
            var repository = new Mock<IRepository<Exercise>>();
            repository.Setup(x =>
                x.GetAll(
                    null,
                    null,
                    It.IsAny<Func<IQueryable<Exercise>, IIncludableQueryable<Exercise, object>>>(),
                    true,
                    CancellationToken.None
            )).ReturnsAsync(exercises);

            var logger = new NullLogger<ExerciseService>();

            var service = new ExerciseService(repository: repository.Object, logger: logger);

            // ACT
            var result = (await service.GetAll(CancellationToken.None)).ToList();

            // ASSERT
            Assert.IsType<List<Exercise>>(result);
            Assert.Equal(exercises[0].Id, result[0].Id);
            Assert.Equal(exercises[1].Id, result[1].Id);

            repository.Verify(x => x.GetAll(null,
                    null,
                    It.IsAny<Func<IQueryable<Exercise>, IIncludableQueryable<Exercise, object>>>(),
                    true,
                    CancellationToken.None)
            , Times.Once);

            repository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Get_TwoExercisesInDb_GetsOne()
        {
            var exercises = new List<Exercise>()
            {
                new Exercise()
                {
                    Id = Guid.Parse("47b176a6-535f-447b-9f09-86465f07967a"),
                    Name = "Bench Press",
                    Weight = 100,
                    Sets = 5,
                    Reps = 5,
                    TrainingLogId = Guid.Parse("cf4509d5-630c-470e-81c0-22628863c0d8"),
                },
                new Exercise()
                {
                    Id = Guid.Parse("d386146c-06ac-4325-82fa-11721ecb1d4b"),
                    Name = "Squat",
                    Weight = 140,
                    Sets = 3,
                    Reps = 5,
                    TrainingLogId = Guid.Parse("cf4509d5-630c-470e-81c0-22628863c0d8"),
                },
            };

            // ARRANGE
            var repository = new Mock<IRepository<Exercise>>();
            var id = exercises[1].Id;
            repository.Setup(x =>
                x.Get(
                    dbLog => dbLog.Id == id,
                    null,
                    It.IsAny<Func<IQueryable<Exercise>, IIncludableQueryable<Exercise, object>>>(),
                    true,
                    CancellationToken.None
            )).ReturnsAsync(exercises[1]);

            var logger = new NullLogger<ExerciseService>();

            var service = new ExerciseService(repository: repository.Object, logger: logger);

            // ACT
            var result = await service.Get(id, CancellationToken.None);

            // ASSERT
            Assert.IsType<Exercise>(result);
            Assert.Equal(exercises[1], result);

            repository.Verify(x => x.Get(
                    dbLog => dbLog.Id == id,
                    null,
                    It.IsAny<Func<IQueryable<Exercise>, IIncludableQueryable<Exercise, object>>>(),
                    true,
                    CancellationToken.None)
            , Times.Once);

            repository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Get_TwoExercisesInDb_ThrowsNotFoundException()
        {
            var exercises = new List<Exercise>()
            {
                new Exercise()
                {
                    Id = Guid.Parse("47b176a6-535f-447b-9f09-86465f07967a"),
                    Name = "Bench Press",
                    Weight = 100,
                    Sets = 5,
                    Reps = 5,
                    TrainingLogId = Guid.Parse("cf4509d5-630c-470e-81c0-22628863c0d8"),
                },
                new Exercise()
                {
                    Id = Guid.Parse("d386146c-06ac-4325-82fa-11721ecb1d4b"),
                    Name = "Squat",
                    Weight = 140,
                    Sets = 3,
                    Reps = 5,
                    TrainingLogId = Guid.Parse("cf4509d5-630c-470e-81c0-22628863c0d8"),
                },
            };

            // ARRANGE
            var repository = new Mock<IRepository<Exercise>>();
            var exerciseId = Guid.Parse("689f9a14-de94-424e-945f-54c92518e2e6");

            var exception = new NotFoundException(exerciseId);

            repository.Setup(x =>
                x.Get(
                    dbExercise => dbExercise.Id == exerciseId,
                    null,
                    It.IsAny<Func<IQueryable<Exercise>, IIncludableQueryable<Exercise, object>>>(),
                    true,
                    CancellationToken.None
            )).ReturnsAsync((Exercise)null);

            var logger = new NullLogger<ExerciseService>();

            var service = new ExerciseService(repository: repository.Object, logger: logger);

            // ACT/ASSERT
            await Assert.ThrowsAsync<NotFoundException>(() => service.Get(exerciseId, CancellationToken.None));

            repository.Verify(x => x.Get(
                    dbLog => dbLog.Id == exerciseId,
                    null,
                    It.IsAny<Func<IQueryable<Exercise>, IIncludableQueryable<Exercise, object>>>(),
                    true,
                    CancellationToken.None)
            , Times.Once);

            repository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Create_AddExerciseToDb_InsertsExercise()
        {
            var exercise = new Exercise()
            {
                Id = Guid.Parse("47b176a6-535f-447b-9f09-86465f07967a"),
                Name = "Bench Press",
                Weight = 100,
                Sets = 5,
                Reps = 5,
                TrainingLogId = Guid.Parse("cf4509d5-630c-470e-81c0-22628863c0d8"),
            };

            // ARRANGE
            var repository = new Mock<IRepository<Exercise>>();
            repository.Setup(x =>
                x.Insert(
                    It.IsAny<Exercise>(),
                    CancellationToken.None
            )).ReturnsAsync(exercise.Id);

            var logger = new NullLogger<ExerciseService>();

            var service = new ExerciseService(repository: repository.Object, logger: logger);

            // ACT
            var result = await service.Create(exercise, CancellationToken.None);

            // ASSERT
            Assert.IsType<Exercise>(result);
            Assert.Equal(exercise.Id, result.Id);
            repository.Verify(x => x.Insert(
                    It.IsAny<Exercise>(),
                    CancellationToken.None)
            , Times.Once);

            repository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Create_AddExerciseToDb_ThrowsCreateException()
        {
            var exercise = new Exercise()
            {
                Id = Guid.Parse("47b176a6-535f-447b-9f09-86465f07967a"),
                Name = "Bench Press",
                Weight = 100,
                Sets = 5,
                Reps = 5,
                TrainingLogId = Guid.Parse("cf4509d5-630c-470e-81c0-22628863c0d8"),
            };

            var exception = new CreateException();

            // ARRANGE
            var repository = new Mock<IRepository<Exercise>>();
            repository.Setup(x =>
                x.Insert(
                    It.IsAny<Exercise>(),
                    CancellationToken.None
            )).ThrowsAsync(exception);

            var logger = new NullLogger<ExerciseService>();

            var service = new ExerciseService(repository: repository.Object, logger: logger);

            // ACT/ASSERT
            await Assert.ThrowsAsync<CreateException>(() => service.Create(exercise, CancellationToken.None));

            repository.Verify(x => x.Insert(
                    It.IsAny<Exercise>(),
                    CancellationToken.None)
            , Times.Once);

            repository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Create_AddExerciseToDb_ThrowsValidationException()
        {
            var exercise = new Exercise()
            {
                Id = Guid.Parse("47b176a6-535f-447b-9f09-86465f07967a"),
                Name = "Bench Press",
                Weight = -30,
                Sets = 5,
                Reps = 5,
                TrainingLogId = Guid.Parse("cf4509d5-630c-470e-81c0-22628863c0d8"),
            };

            var exception = new ValidationException("log");

            // ARRANGE
            var repository = new Mock<IRepository<Exercise>>();
            repository.Setup(x =>
                x.Insert(
                    It.IsAny<Exercise>(),
                    CancellationToken.None
            )).ThrowsAsync(exception);

            var logger = new NullLogger<ExerciseService>();

            var service = new ExerciseService(repository: repository.Object, logger: logger);

            // ACT/ASSERT
            await Assert.ThrowsAsync<ValidationException>(async () => await service.Create(exercise, CancellationToken.None));

            repository.Verify(x => x.Insert(
                    It.IsAny<Exercise>(),
                    CancellationToken.None)
            , Times.Never);

            repository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Delete_DeleteExerciseFromDb_DeleteExercise()
        {
            var exerciseId = new Guid("f86b3510-3466-47a1-9a6d-8325b8b305c5");
            var inputGuidToAssert = Guid.Empty;

            // ARRANGE 
            var repository = new Mock<IRepository<Exercise>>();
            var setup = repository.Setup(x =>
                x.Delete(
                    It.IsAny<Guid>(),
                    CancellationToken.None));

            setup.Callback((Guid guid, CancellationToken token) => inputGuidToAssert = guid);

            var logger = new NullLogger<ExerciseService>();

            var service = new ExerciseService(repository.Object, logger);

            // ACT
            await service.Delete(exerciseId, CancellationToken.None);

            // ASSERT
            Assert.Equal(exerciseId, inputGuidToAssert);
            repository.Verify(x => x.Delete(
                    It.IsAny<Guid>(),
                    CancellationToken.None)
            , Times.Once);

            repository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Delete_DeleteExerciseFromDb_ThrowsDeleteException()
        {
            var exerciseId = new Guid("f86b3510-3466-47a1-9a6d-8325b8b305c5");
            var inputGuidToAssert = Guid.Empty;
            var exception = new DeleteException(exerciseId);

            // ARRANGE 
            var repository = new Mock<IRepository<Exercise>>();
            repository.Setup(x =>
                x.Delete(
                    It.IsAny<Guid>(),
                    CancellationToken.None)).Throws(exception);

            var logger = new NullLogger<ExerciseService>();

            var service = new ExerciseService(repository.Object, logger);

            // ACT/ASSERT
            await Assert.ThrowsAsync<DeleteException>(async () => await service.Delete(exerciseId, CancellationToken.None));

            repository.Verify(x => x.Delete(
                    It.IsAny<Guid>(),
                    CancellationToken.None)
            , Times.Once);

            repository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Update_ExerciseLogInDb_UpdatesExercise()
        {
            var exercise = new Exercise()
            {
                Id = Guid.Parse("47b176a6-535f-447b-9f09-86465f07967a"),
                Name = "Bench Press",
                Weight = 100,
                Sets = 5,
                Reps = 5,
                TrainingLogId = Guid.Parse("cf4509d5-630c-470e-81c0-22628863c0d8"),
            };

            // ARRANGE
            var repository = new Mock<IRepository<Exercise>>();
            var setup = repository.Setup(x =>
                x.Update(
                    It.IsAny<Exercise>(),
                    CancellationToken.None
            ));

            var logger = new NullLogger<ExerciseService>();

            var service = new ExerciseService(repository: repository.Object, logger: logger);

            // ACT
            var result = await service.Update(exercise, CancellationToken.None);

            // ASSERT
            Assert.IsType<Exercise>(result);
            Assert.Equal(exercise, result);
            repository.Verify(x => x.Update(
                    It.IsAny<Exercise>(),
                    CancellationToken.None)
            , Times.Once);

            repository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Update_UpdateExerciseInDb_ThrowsUpdateException()
        {
            var exercise = new Exercise()
            {
                Id = Guid.Parse("47b176a6-535f-447b-9f09-86465f07967a"),
                Name = "Bench Press",
                Weight = 100,
                Sets = 5,
                Reps = 5,
                TrainingLogId = Guid.Parse("cf4509d5-630c-470e-81c0-22628863c0d8"),
            };

            var exception = new UpdateException(exercise.Id);

            // ARRANGE
            var repository = new Mock<IRepository<Exercise>>();
            repository.Setup(x =>
                x.Update(
                    It.IsAny<Exercise>(),
                    CancellationToken.None
            )).Throws(exception);

            var logger = new NullLogger<ExerciseService>();

            var service = new ExerciseService(repository: repository.Object, logger: logger);

            // ACT/ASSERT
            await Assert.ThrowsAsync<UpdateException>(async () => await service.Update(exercise, CancellationToken.None));

            repository.Verify(x => x.Update(
                    It.IsAny<Exercise>(),
                    CancellationToken.None)
            , Times.Once);

            repository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Update_UpdateExerciseInDb_ThrowsValidationException()
        {
            var exercise = new Exercise()
            {
                Id = Guid.Parse("47b176a6-535f-447b-9f09-86465f07967a"),
                Name = "Bench Press",
                Weight = -30,
                Sets = 5,
                Reps = 5,
                TrainingLogId = Guid.Parse("cf4509d5-630c-470e-81c0-22628863c0d8"),
            };

            var exception = new ValidationException("log");

            // ARRANGE
            var repository = new Mock<IRepository<Exercise>>();
            repository.Setup(x =>
                x.Update(
                    It.IsAny<Exercise>(),
                    CancellationToken.None
            )).ThrowsAsync(exception);

            var logger = new NullLogger<ExerciseService>();

            var service = new ExerciseService(repository: repository.Object, logger: logger);

            // ACT/ASSERT
            await Assert.ThrowsAsync<ValidationException>(() => service.Update(exercise, CancellationToken.None));

            repository.Verify(x => x.Update(
                    It.IsAny<Exercise>(),
                    CancellationToken.None)
            , Times.Never);

            repository.VerifyNoOtherCalls();
        }
    }
}
