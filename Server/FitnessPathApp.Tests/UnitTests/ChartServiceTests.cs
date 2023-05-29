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
    public class ChartServiceTests
    {
        [Fact]
        public async Task GetMonthlyDataByExerciseName_TwoTrainingLogsInDb_GetsBoth()
        {
            var logs = new List<TrainingLog>()
            {
                new TrainingLog()
                {
                    Id = Guid.Parse("47b176a6-535f-447b-9f09-86465f07967a"),
                    Date = new DateTime(2022 ,9, 4),
                    Exercises = new List<Exercise>
                    {
                        new Exercise()
                        {
                            Id = Guid.Parse("b91474ad-0825-4f0e-8532-96a724978e91"),
                            Name = "Bench Press",
                            Weight = 100,
                            Sets = 5,
                            Reps = 5,
                            TrainingLogId = Guid.Parse("47b176a6-535f-447b-9f09-86465f07967a"),
                        }
                    }
                },
                new TrainingLog()
                {
                    Id = Guid.Parse("d386146c-06ac-4325-82fa-11721ecb1d4b"),
                    Date = new DateTime(2022 ,9, 6),
                     Exercises = new List<Exercise>
                    {
                        new Exercise()
                        {
                            Id = Guid.Parse("fea76b9c-9c6f-4664-83ca-417609733c54"),
                            Name = "Bench Press",
                            Weight = 120,
                            Sets = 5,
                            Reps = 5,
                            TrainingLogId = Guid.Parse("d386146c-06ac-4325-82fa-11721ecb1d4b"),
                        }
                    }
                }
            };

            // ARRANGE
            var trainingRepository = new Mock<IRepository<TrainingLog>>();
            trainingRepository.Setup(x =>
                x.GetAll(
                    source => source.Exercises.Count != 0 &&
                                  source.Exercises.Where(exercise => exercise.Name == "Bench Press").ToList().Count != 0 &&
                                  source.Date.Year == 2022 &&
                                  source.Date.Month == 9,
                    It.IsAny<Func<IQueryable<TrainingLog>, IQueryable<TrainingLog>>>(),
                    It.IsAny<Func<IQueryable<TrainingLog>, IIncludableQueryable<TrainingLog, object>>>(),
                    true,
                    CancellationToken.None
            )).ReturnsAsync(logs); ;

            

            var weightRepository = new Mock<IRepository<WeightLog>>();

            var logger = new NullLogger<ChartService>();

            var service = new ChartService(trainingRepository: trainingRepository.Object, weightRepository: weightRepository.Object, logger: logger);

            var dataArray = new List<ChartPoint> {
                new ChartPoint
            {
                Y = 100,
                X = 4
            }, new ChartPoint
            {
                Y = 120,
                X = 6
            } };

            // ACT
            var result = (await service.GetMonthlyDataByExerciseName("Bench Press", 9, 2022, CancellationToken.None));
            var expectedResult = new ChartDataDTO
            {
                Data = dataArray,
                YMax = 120,
                YMin = 100,
                XMin = 1,
                XMax = DateTime.Now.Day,
                TickAmount = (int)((120 - 100) / 2.5),
                ProgressPercentage = 20,
            };

            // ASSERT
            Assert.IsType<ChartDataDTO>(result);
            Assert.Equivalent(expectedResult, result, true);

            trainingRepository.Verify(x => x.GetAll(
                    source => source.Exercises.Count != 0 &&
                                  source.Exercises.Where(exercise => exercise.Name == "Bench Press").ToList().Count != 0 &&
                                  source.Date.Year == 2022 &&
                                  source.Date.Month == 9,
                    It.IsAny<Func<IQueryable<TrainingLog>, IQueryable<TrainingLog>>>(),
                    It.IsAny<Func<IQueryable<TrainingLog>, IIncludableQueryable<TrainingLog, object>>>(),
                    true,
                    CancellationToken.None)
            , Times.Once);

            trainingRepository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task GetYearlyDataByExerciseName_TwoTrainingLogsInDb_GetsBoth()
        {
            var logs = new List<TrainingLog>()
            {
                new TrainingLog()
                {
                    Id = Guid.Parse("47b176a6-535f-447b-9f09-86465f07967a"),
                    Date = new DateTime(2022 ,9, 4),
                    Exercises = new List<Exercise>
                    {
                        new Exercise()
                        {
                            Id = Guid.Parse("b91474ad-0825-4f0e-8532-96a724978e91"),
                            Name = "Bench Press",
                            Weight = 100,
                            Sets = 5,
                            Reps = 5,
                            TrainingLogId = Guid.Parse("47b176a6-535f-447b-9f09-86465f07967a"),
                        }
                    }
                },
                new TrainingLog()
                {
                    Id = Guid.Parse("d386146c-06ac-4325-82fa-11721ecb1d4b"),
                    Date = new DateTime(2022 ,10, 6),
                     Exercises = new List<Exercise>
                    {
                        new Exercise()
                        {
                            Id = Guid.Parse("fea76b9c-9c6f-4664-83ca-417609733c54"),
                            Name = "Bench Press",
                            Weight = 120,
                            Sets = 5,
                            Reps = 5,
                            TrainingLogId = Guid.Parse("d386146c-06ac-4325-82fa-11721ecb1d4b"),
                        }
                    }
                }
            };

            // ARRANGE
            var trainingRepository = new Mock<IRepository<TrainingLog>>();
            trainingRepository.Setup(x =>
                x.GetAll(
                    source => source.Exercises.Count != 0 &&
                                  source.Exercises.Where(exercise => exercise.Name == "Bench Press").ToList().Count != 0 &&
                                  source.Date.Year == 2022,
                    It.IsAny<Func<IQueryable<TrainingLog>, IQueryable<TrainingLog>>>(),
                    It.IsAny<Func<IQueryable<TrainingLog>, IIncludableQueryable<TrainingLog, object>>>(),
                    true,
                    CancellationToken.None
            )).ReturnsAsync(logs); ;



            var weightRepository = new Mock<IRepository<WeightLog>>();

            var logger = new NullLogger<ChartService>();

            var service = new ChartService(trainingRepository: trainingRepository.Object, weightRepository: weightRepository.Object, logger: logger);

            var dataArray = new List<ChartPoint> {
                new ChartPoint
            {
                Y = 100,
                X = 9
            }, new ChartPoint
            {
                Y = 120,
                X = 10
            } };

            // ACT
            var result = (await service.GetYearlyDataByExerciseName("Bench Press", 2022, CancellationToken.None));
            var expectedResult = new ChartDataDTO
            {
                Data = dataArray,
                YMax = 120,
                YMin = 100,
                XMin = 1,
                XMax = DateTime.Now.Day,
                TickAmount = (int)((120 - 100) / 2.5),
                ProgressPercentage = 20,
            };

            // ASSERT
            Assert.IsType<ChartDataDTO>(result);
            Assert.Equivalent(expectedResult, result, true);

            trainingRepository.Verify(x => x.GetAll(
                    source => source.Exercises.Count != 0 &&
                                  source.Exercises.Where(exercise => exercise.Name == "Bench Press").ToList().Count != 0 &&
                                  source.Date.Year == 2022,
                    It.IsAny<Func<IQueryable<TrainingLog>, IQueryable<TrainingLog>>>(),
                    It.IsAny<Func<IQueryable<TrainingLog>, IIncludableQueryable<TrainingLog, object>>>(),
                    true,
                    CancellationToken.None)
            , Times.Once);

            trainingRepository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task GetYearlyWeightChangeData_TwoWeightLogsInDb_GetsBoth()
        {
            var logs = new List<WeightLog>()
            {
                new WeightLog()
                {
                    Id = Guid.Parse("47b176a6-535f-447b-9f09-86465f07967a"),
                    Date = new DateTime(2022 ,9, 4),
                    Value = 80
                },
                new WeightLog()
                {
                    Id = Guid.Parse("d386146c-06ac-4325-82fa-11721ecb1d4b"),
                    Date = new DateTime(2022 ,10, 6),
                    Value = 81.5
                }
            };

            // ARRANGE
            var trainingRepository = new Mock<IRepository<TrainingLog>>();

            var weightRepository = new Mock<IRepository<WeightLog>>();

            weightRepository.Setup(x =>
                x.GetAll(source =>
                                  source.Date.Year == 2022,
                                  It.IsAny<Func<IQueryable<WeightLog>, IQueryable<WeightLog>>>(),
                                  It.IsAny<Func<IQueryable<WeightLog>, IIncludableQueryable<WeightLog, object>>>(),
                                  true,
                                  CancellationToken.None)).ReturnsAsync(logs);

            var logger = new NullLogger<ChartService>();

            var service = new ChartService(trainingRepository: trainingRepository.Object, weightRepository: weightRepository.Object, logger: logger);

            var dataArray = new List<ChartPoint> {
                new ChartPoint
            {
                Y = 80,
                X = 9
            }, new ChartPoint
            {
                Y = 81.5,
                X = 10
            } };

            // ACT
            var result = (await service.GetYearlyWeightChangeData(2022, CancellationToken.None));
            var expectedResult = new ChartDataDTO
            {
                Data = dataArray,
                YMax = 81.5,
                YMin = 80,
                XMin = 1,
                XMax = DateTime.Now.Day,
                TickAmount = (int)((81.5 - 80) / 2.5),
                ProgressPercentage = 1.875,
            };

            // ASSERT
            Assert.IsType<ChartDataDTO>(result);
            Assert.Equivalent(expectedResult, result, true);

            weightRepository.Verify(x => x.GetAll(
                            source => source.Date.Year == 2022,
                            It.IsAny<Func<IQueryable<WeightLog>, IQueryable<WeightLog>>>(),
                            It.IsAny<Func<IQueryable<WeightLog>, IIncludableQueryable<WeightLog, object>>>(),
                            true,
                            CancellationToken.None)
            , Times.Once);

            weightRepository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task GetMonthlyWeightChangeData_TwoWeightLogsInDb_GetsBoth()
        {
            var logs = new List<WeightLog>()
            {
                new WeightLog()
                {
                    Id = Guid.Parse("47b176a6-535f-447b-9f09-86465f07967a"),
                    Date = new DateTime(2022 ,9, 4),
                    Value = 80
                },
                new WeightLog()
                {
                    Id = Guid.Parse("d386146c-06ac-4325-82fa-11721ecb1d4b"),
                    Date = new DateTime(2022 ,9, 6),
                    Value = 81.5
                }
            };

            // ARRANGE
            var trainingRepository = new Mock<IRepository<TrainingLog>>();

            var weightRepository = new Mock<IRepository<WeightLog>>();

            weightRepository.Setup(x =>
                x.GetAll(source =>
                                  source.Date.Year == 2022 && source.Date.Month == 9,
                                  It.IsAny<Func<IQueryable<WeightLog>, IQueryable<WeightLog>>>(),
                                  It.IsAny<Func<IQueryable<WeightLog>, IIncludableQueryable<WeightLog, object>>>(),
                                  true,
                                  CancellationToken.None)).ReturnsAsync(logs);

            var logger = new NullLogger<ChartService>();

            var service = new ChartService(trainingRepository: trainingRepository.Object, weightRepository: weightRepository.Object, logger: logger);

            var dataArray = new List<ChartPoint> {
                new ChartPoint
            {
                Y = 80,
                X = 4
            }, new ChartPoint
            {
                Y = 81.5,
                X = 6
            } };

            // ACT
            var result = (await service.GetMonthlyWeightChangeData(9, 2022, CancellationToken.None));
            var expectedResult = new ChartDataDTO
            {
                Data = dataArray,
                YMax = 81.5,
                YMin = 80,
                XMin = 1,
                XMax = DateTime.Now.Day,
                TickAmount = (int)((81.5 - 80) / 2.5),
                ProgressPercentage = 1.875,
            };

            // ASSERT
            Assert.IsType<ChartDataDTO>(result);
            Assert.Equivalent(expectedResult, result, true);

            weightRepository.Verify(x => x.GetAll(
                            source => source.Date.Year == 2022 && source.Date.Month == 9,
                            It.IsAny<Func<IQueryable<WeightLog>, IQueryable<WeightLog>>>(),
                            It.IsAny<Func<IQueryable<WeightLog>, IIncludableQueryable<WeightLog, object>>>(),
                            true,
                            CancellationToken.None)
            , Times.Once);

            weightRepository.VerifyNoOtherCalls();
        }
    }
}
