using FitnessPathApp.BusinessLayer.Interfaces;
using FitnessPathApp.DomainLayer.Entities;
using FitnessPathApp.PersistanceLayer.DTOs;
using FitnessPathApp.PersistanceLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FitnessPathApp.BusinessLayer.Implementations
{
    public class ChartService : IChartService
    {
        private readonly IRepository<TrainingLog> _repository;
        private readonly ILogger<ChartService> _logger;
        public ChartService(IRepository<TrainingLog> repository, ILogger<ChartService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<ChartDataDTO> GetDataByExerciseName(string exerciseName, int month, CancellationToken cancellationToken)
        {
            var logs = await _repository.GetAll(
                 filter: source => source.Exercises.Count != 0 &&
                                   source.Exercises.Where( exercise => exercise.Name == exerciseName).ToList().Count != 0 &&
                                   source.Date.Year == DateTime.Now.Year,
                 include: source => source.Include(log => log.Exercises),
                 orderBy: source => source.OrderBy(log => log.Date),
                 cancellationToken: cancellationToken);

            var dataArray = logs.Select(log => new ChartPoint { Y = log.Exercises.ToArray()[0].Weight, X = log.Date.Day });

            var yMin = dataArray.Select(point => point.Y).Min();
            var yMax = dataArray.Select(point => point.Y).Max();

            int tickAmount = (int)((yMax - yMin) / 2.5);

            ChartDataDTO chartData = new ChartDataDTO
            {
                Data = dataArray,
                YMax = yMax,
                YMin = yMin,
                TickAmount = tickAmount
            };

            return chartData;
        }
    }
}
