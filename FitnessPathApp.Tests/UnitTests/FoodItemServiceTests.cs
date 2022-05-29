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

namespace FitnessPathApp.Tests.UnitTests
{
    public class FoodItemServiceTests
    {
        [Fact]
        public async Task GetAll_TwoFoodItemsInDb_GetsBoth()
        {
            var items = new List<FoodItem>()
            {
                new FoodItem()
                {
                    Id = Guid.Parse("47b176a6-535f-447b-9f09-86465f07967a"),
                    Name = "Chicken",
                    Calories = 300,
                    Carbs = 10,
                    Protein = 22,
                    Fat = 5,
                    FoodLogId = Guid.Parse("cf4509d5-630c-470e-81c0-22628863c0d8"),
                },
                new FoodItem()
                {
                    Id = Guid.Parse("d386146c-06ac-4325-82fa-11721ecb1d4b"),
                    Name = "Rice",
                    Calories = 300,
                    Carbs = 80,
                    Protein = 20,
                    Fat = 2,
                    FoodLogId = Guid.Parse("cf4509d5-630c-470e-81c0-22628863c0d8"),
                },
            };

            // ARRANGE
            var repository = new Mock<IRepository<FoodItem>>();
            repository.Setup(x =>
                x.GetAll(
                    null,
                    null,
                    It.IsAny<Func<IQueryable<FoodItem>, IIncludableQueryable<FoodItem, object>>>(),
                    true,
                    CancellationToken.None
            )).ReturnsAsync(items);

            var logger = new NullLogger<FoodItemService>();

            var service = new FoodItemService(repository: repository.Object, logger: logger);

            // ACT
            var result = (await service.GetAll(CancellationToken.None)).ToList();

            // ASSERT
            Assert.IsType<List<FoodItem>>(result);
            Assert.Equal(items[0].Id, result[0].Id);
            Assert.Equal(items[1].Id, result[1].Id);

            repository.Verify(x => x.GetAll(null,
                    null,
                    It.IsAny<Func<IQueryable<FoodItem>, IIncludableQueryable<FoodItem, object>>>(),
                    true,
                    CancellationToken.None)
            , Times.Once);

            repository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Get_TwoFoodItemsInDb_GetsOne()
        {
            var items = new List<FoodItem>()
            {
                new FoodItem()
                {
                    Id = Guid.Parse("47b176a6-535f-447b-9f09-86465f07967a"),
                    Name = "Chicken",
                    Calories = 300,
                    Carbs = 10,
                    Protein = 22,
                    Fat = 5,
                    FoodLogId = Guid.Parse("cf4509d5-630c-470e-81c0-22628863c0d8"),
                },
                new FoodItem()
                {
                    Id = Guid.Parse("d386146c-06ac-4325-82fa-11721ecb1d4b"),
                    Name = "Rice",
                    Calories = 300,
                    Carbs = 80,
                    Protein = 20,
                    Fat = 2,
                    FoodLogId = Guid.Parse("cf4509d5-630c-470e-81c0-22628863c0d8"),
                }
            };

            // ARRANGE
            var repository = new Mock<IRepository<FoodItem>>();
            var id = items[1].Id;
            repository.Setup(x =>
                x.Get(
                    dbLog => dbLog.Id == id,
                    null,
                    It.IsAny<Func<IQueryable<FoodItem>, IIncludableQueryable<FoodItem, object>>>(),
                    true,
                    CancellationToken.None
            )).ReturnsAsync(items[1]);

            var logger = new NullLogger<FoodItemService>();

            var service = new FoodItemService(repository: repository.Object, logger: logger);

            // ACT
            var result = await service.Get(id, CancellationToken.None);

            // ASSERT
            Assert.IsType<FoodItem>(result);
            Assert.Equal(items[1], result);

            repository.Verify(x => x.Get(
                    dbLog => dbLog.Id == id,
                    null,
                    It.IsAny<Func<IQueryable<FoodItem>, IIncludableQueryable<FoodItem, object>>>(),
                    true,
                    CancellationToken.None)
            , Times.Once);

            repository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Get_TwoFoodItemsInDb_ThrowsNotFoundException()
        {
            var items = new List<FoodItem>()
            {
                new FoodItem()
                {
                    Id = Guid.Parse("47b176a6-535f-447b-9f09-86465f07967a"),
                    Name = "Chicken",
                    Calories = 300,
                    Carbs = 10,
                    Protein = 22,
                    Fat = 5,
                    FoodLogId = Guid.Parse("cf4509d5-630c-470e-81c0-22628863c0d8"),
                },
                new FoodItem()
                {
                    Id = Guid.Parse("d386146c-06ac-4325-82fa-11721ecb1d4b"),
                    Name = "Rice",
                    Calories = 300,
                    Carbs = 80,
                    Protein = 20,
                    Fat = 2,
                    FoodLogId = Guid.Parse("cf4509d5-630c-470e-81c0-22628863c0d8"),
                }
            };

            // ARRANGE
            var repository = new Mock<IRepository<FoodItem>>();
            var itemId = Guid.Parse("689f9a14-de94-424e-945f-54c92518e2e6");

            var exception = new NotFoundException(itemId);

            repository.Setup(x =>
                x.Get(
                    dbItem => dbItem.Id == itemId,
                    null,
                    It.IsAny<Func<IQueryable<FoodItem>, IIncludableQueryable<FoodItem, object>>>(),
                    true,
                    CancellationToken.None
            )).ReturnsAsync((FoodItem)null);

            var logger = new NullLogger<FoodItemService>();

            var service = new FoodItemService(repository: repository.Object, logger: logger);

            // ACT/ASSERT
            await Assert.ThrowsAsync<NotFoundException>(() => service.Get(itemId, CancellationToken.None));

            repository.Verify(x => x.Get(
                    dbItem => dbItem.Id == itemId,
                    null,
                    It.IsAny<Func<IQueryable<FoodItem>, IIncludableQueryable<FoodItem, object>>>(),
                    true,
                    CancellationToken.None)
            , Times.Once);

            repository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Create_AddFoodItemToDb_InsertsFoodItem()
        {
            var item = new FoodItem()
            {
                Id = Guid.Parse("47b176a6-535f-447b-9f09-86465f07967a"),
                Name = "Chicken",
                Calories = 300,
                Carbs = 10,
                Protein = 22,
                Fat = 5,
                FoodLogId = Guid.Parse("cf4509d5-630c-470e-81c0-22628863c0d8"),
            };

            // ARRANGE
            var repository = new Mock<IRepository<FoodItem>>();
            repository.Setup(x =>
                x.Insert(
                    It.IsAny<FoodItem>(),
                    CancellationToken.None
            )).ReturnsAsync(item.Id);

            var logger = new NullLogger<FoodItemService>();

            var service = new FoodItemService(repository: repository.Object, logger: logger);

            // ACT
            var result = await service.Create(item, CancellationToken.None);

            // ASSERT
            Assert.IsType<FoodItem>(result);
            Assert.Equal(item.Id, result.Id);
            repository.Verify(x => x.Insert(
                    It.IsAny<FoodItem>(),
                    CancellationToken.None)
            , Times.Once);

            repository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Create_AddFoodItemToDb_ThrowsCreateException()
        {
            var item = new FoodItem()
            {
                Id = Guid.Parse("47b176a6-535f-447b-9f09-86465f07967a"),
                Name = "Chicken",
                Calories = 300,
                Carbs = 10,
                Protein = 22,
                Fat = 5,
                FoodLogId = Guid.Parse("cf4509d5-630c-470e-81c0-22628863c0d8"),
            };

            var exception = new CreateException();

            // ARRANGE
            var repository = new Mock<IRepository<FoodItem>>();
            repository.Setup(x =>
                x.Insert(
                    It.IsAny<FoodItem>(),
                    CancellationToken.None
            )).ThrowsAsync(exception);

            var logger = new NullLogger<FoodItemService>();

            var service = new FoodItemService(repository: repository.Object, logger: logger);

            // ACT/ASSERT
            await Assert.ThrowsAsync<CreateException>(() => service.Create(item, CancellationToken.None));

            repository.Verify(x => x.Insert(
                    It.IsAny<FoodItem>(),
                    CancellationToken.None)
            , Times.Once);

            repository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Create_AddFoodItemToDb_ThrowsValidationException()
        {
            var item = new FoodItem()
            {
                Id = Guid.Parse("47b176a6-535f-447b-9f09-86465f07967a"),
                Name = "Chicken",
                Calories = -300,
                Carbs = 10,
                Protein = 22,
                Fat = 5,
                FoodLogId = Guid.Parse("cf4509d5-630c-470e-81c0-22628863c0d8"),
            };

            var exception = new ValidationException("log");

            // ARRANGE
            var repository = new Mock<IRepository<FoodItem>>();
            repository.Setup(x =>
                x.Insert(
                    It.IsAny<FoodItem>(),
                    CancellationToken.None
            )).ThrowsAsync(exception);

            var logger = new NullLogger<FoodItemService>();

            var service = new FoodItemService(repository: repository.Object, logger: logger);

            // ACT/ASSERT
            await Assert.ThrowsAsync<ValidationException>(async () => await service.Create(item, CancellationToken.None));

            repository.Verify(x => x.Insert(
                    It.IsAny<FoodItem>(),
                    CancellationToken.None)
            , Times.Never);

            repository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Delete_DeleteFoodItemFromDb_DeleteFoodItem()
        {
            var itemId = new Guid("f86b3510-3466-47a1-9a6d-8325b8b305c5");
            var inputGuidToAssert = Guid.Empty;

            // ARRANGE 
            var repository = new Mock<IRepository<FoodItem>>();
            var setup = repository.Setup(x =>
                x.Delete(
                    It.IsAny<Guid>(),
                    CancellationToken.None));

            setup.Callback((Guid guid, CancellationToken token) => inputGuidToAssert = guid);

            var logger = new NullLogger<FoodItemService>();

            var service = new FoodItemService(repository.Object, logger);

            // ACT
            await service.Delete(itemId, CancellationToken.None);

            // ASSERT
            Assert.Equal(itemId, inputGuidToAssert);
            repository.Verify(x => x.Delete(
                    It.IsAny<Guid>(),
                    CancellationToken.None)
            , Times.Once);

            repository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Delete_DeleteFoodItemFromDb_ThrowsDeleteException()
        {
            var itemId = new Guid("f86b3510-3466-47a1-9a6d-8325b8b305c5");
            var inputGuidToAssert = Guid.Empty;
            var exception = new DeleteException(itemId);

            // ARRANGE 
            var repository = new Mock<IRepository<FoodItem>>();
            repository.Setup(x =>
                x.Delete(
                    It.IsAny<Guid>(),
                    CancellationToken.None)).Throws(exception);

            var logger = new NullLogger<FoodItemService>();

            var service = new FoodItemService(repository.Object, logger);

            // ACT/ASSERT
            await Assert.ThrowsAsync<DeleteException>(async () => await service.Delete(itemId, CancellationToken.None));

            repository.Verify(x => x.Delete(
                    It.IsAny<Guid>(),
                    CancellationToken.None)
            , Times.Once);

            repository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Update_FoodItemInDb_UpdatesFoodItem()
        {
            var item = new FoodItem()
            {
                Id = Guid.Parse("47b176a6-535f-447b-9f09-86465f07967a"),
                Name = "Chicken",
                Calories = 300,
                Carbs = 10,
                Protein = 22,
                Fat = 5,
                FoodLogId = Guid.Parse("cf4509d5-630c-470e-81c0-22628863c0d8"),
            };

            // ARRANGE
            var repository = new Mock<IRepository<FoodItem>>();
            var setup = repository.Setup(x =>
                x.Update(
                    It.IsAny<FoodItem>(),
                    CancellationToken.None
            ));

            var logger = new NullLogger<FoodItemService>();

            var service = new FoodItemService(repository: repository.Object, logger: logger);

            // ACT
            var result = await service.Update(item, CancellationToken.None);

            // ASSERT
            Assert.IsType<FoodItem>(result);
            Assert.Equal(item, result);
            repository.Verify(x => x.Update(
                    It.IsAny<FoodItem>(),
                    CancellationToken.None)
            , Times.Once);

            repository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Update_UpdateFoodItemInDb_ThrowsUpdateException()
        {
            var item = new FoodItem()
            {
                Id = Guid.Parse("47b176a6-535f-447b-9f09-86465f07967a"),
                Name = "Chicken",
                Calories = 300,
                Carbs = 10,
                Protein = 22,
                Fat = 5,
                FoodLogId = Guid.Parse("cf4509d5-630c-470e-81c0-22628863c0d8"),
            };

            var exception = new UpdateException(item.Id);

            // ARRANGE
            var repository = new Mock<IRepository<FoodItem>>();
            repository.Setup(x =>
                x.Update(
                    It.IsAny<FoodItem>(),
                    CancellationToken.None
            )).Throws(exception);

            var logger = new NullLogger<FoodItemService>();

            var service = new FoodItemService(repository: repository.Object, logger: logger);

            // ACT/ASSERT
            await Assert.ThrowsAsync<UpdateException>(async () => await service.Update(item, CancellationToken.None));

            repository.Verify(x => x.Update(
                    It.IsAny<FoodItem>(),
                    CancellationToken.None)
            , Times.Once);

            repository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Update_UpdateFoodItemInDb_ThrowsValidationException()
        {
            var item = new FoodItem()
            {
                Id = Guid.Parse("47b176a6-535f-447b-9f09-86465f07967a"),
                Name = "Chicken",
                Calories = -300,
                Carbs = 10,
                Protein = 22,
                Fat = 5,
                FoodLogId = Guid.Parse("cf4509d5-630c-470e-81c0-22628863c0d8"),
            };

            var exception = new ValidationException("log");

            // ARRANGE
            var repository = new Mock<IRepository<FoodItem>>();
            repository.Setup(x =>
                x.Update(
                    It.IsAny<FoodItem>(),
                    CancellationToken.None
            )).ThrowsAsync(exception);

            var logger = new NullLogger<FoodItemService>();

            var service = new FoodItemService(repository: repository.Object, logger: logger);

            // ACT/ASSERT
            await Assert.ThrowsAsync<ValidationException>(() => service.Update(item, CancellationToken.None));

            repository.Verify(x => x.Update(
                    It.IsAny<FoodItem>(),
                    CancellationToken.None)
            , Times.Never);

            repository.VerifyNoOtherCalls();
        }
    }
}
