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
        private readonly IRepository<TrainingLog> _trainingRepository;
        private readonly IRepository<WeightLog> _weightRepository;
        private readonly ILogger<ChartService> _logger;
        public ChartService(IRepository<TrainingLog> trainingRepository, IRepository<WeightLog> weightRepository,
            ILogger<ChartService> logger)
        {
            _weightRepository = weightRepository;
            _trainingRepository = trainingRepository;
            _logger = logger;
        }

        public async Task<ChartDataDTO> GetMonthlyDataByExerciseName(string exerciseName, int month, int year, CancellationToken cancellationToken)
        {

            try
            {
                var logs = await _trainingRepository.GetAll(
                filter: source => source.Exercises.Count != 0 &&
                                  source.Exercises.Where(exercise => exercise.Name == exerciseName).ToList().Count != 0 &&
                                  source.Date.Year == year &&
                                  source.Date.Month == month,
                include: source => source.Include(log => log.Exercises),
                orderBy: source => source.OrderBy(log => log.Date),
                cancellationToken: cancellationToken);

                var dataArray = logs.Select(log => new ChartPoint { Y = log.Exercises.ToArray()[0].Weight, X = log.Date.Day });

                if (dataArray.Count() > 0)
                {
                    var yMin = dataArray.Select(point => point.Y).Min();
                    var yMax = dataArray.Select(point => point.Y).Max();

                    var progressPercentage = (dataArray.Last().Y - dataArray.First().Y) / dataArray.First().Y * 100;

                    int tickAmount = (int)((yMax - yMin) / 2.5);

                    ChartDataDTO chartData = new ChartDataDTO
                    {
                        Data = dataArray,
                        YMax = yMax,
                        YMin = yMin,
                        TickAmount = tickAmount,
                        ProgressPercentage = progressPercentage
                    };

                    return chartData;
                }

                return new ChartDataDTO();
                
            }
            catch(Exception e)
            {
                throw new Exception("No data by given filters");
            }
           
        }

        public async Task<ChartDataDTO> GetYearlyDataByExerciseName(string exerciseName, int year, CancellationToken cancellationToken)
        {

            try
            {
                var logs = await _trainingRepository.GetAll(
                filter: source => source.Exercises.Count != 0 &&
                                  source.Exercises.Where(exercise => exercise.Name == exerciseName).ToList().Count != 0 &&
                                  source.Date.Year == year,
                include: source => source.Include(log => log.Exercises),
                orderBy: source => source.OrderBy(log => log.Date),
                cancellationToken: cancellationToken); ;

                
                var logsGroupedByMonth = logs.AsQueryable().GroupBy(log => new { Month = log.Date.Month}).ToDictionary(g => g.ToList()[0].Date.Month, g => g.ToList()).Values;
                var dataArray = new List<ChartPoint>();

                foreach(var log in logsGroupedByMonth)
                {
                    var exercise = log.Select(log => log.Exercises.Where(log => log.Name == exerciseName).ToList());
                    var month = log.First().Date.Month;

                    var maxWeight = exercise.Select(exercises => exercises[0].Weight).Max();

                    var maxObject = new ChartPoint { Y = maxWeight, X = month };

                    dataArray.Add(maxObject);
                }


                if (dataArray.Count() > 0)
                {
                    var yMin = dataArray.Select(point => point.Y).Min();
                    var yMax = dataArray.Select(point => point.Y).Max();

                    var progressPercentage = (dataArray.Last().Y - dataArray.First().Y) / dataArray.First().Y * 100;

                    int tickAmount = (int)((yMax - yMin) / 2.5);

                    ChartDataDTO chartData = new ChartDataDTO
                    {
                        Data = dataArray,
                        YMax = yMax,
                        YMin = yMin,
                        TickAmount = tickAmount,
                        ProgressPercentage = progressPercentage
                    };

                    return chartData;
                }

                return new ChartDataDTO();

            }
            catch (Exception e)
            {
                throw new Exception("No data by given filters");
            }

        }

        public async Task<ChartDataDTO> GetMonthlyWeightChangeData(int month, int year, CancellationToken cancellationToken)
        {

            try
            {
                var logs = await _weightRepository.GetAll(
                filter: source => 
                                  source.Date.Year == year &&
                                  source.Date.Month == month,
                orderBy: source => source.OrderBy(log => log.Date),
                cancellationToken: cancellationToken);

                var dataArray = logs.Select(log => new ChartPoint { Y = log.Value, X = log.Date.Day });

                if (dataArray.Count() > 0)
                {
                    var yMin = dataArray.Select(point => point.Y).Min();
                    var yMax = dataArray.Select(point => point.Y).Max();

                    var progressPercentage = (dataArray.Last().Y - dataArray.First().Y) / dataArray.First().Y * 100;

                    int tickAmount = (int)((yMax - yMin) / 2.5);

                    ChartDataDTO chartData = new ChartDataDTO
                    {
                        Data = dataArray,
                        YMax = yMax,
                        YMin = yMin,
                        TickAmount = tickAmount,
                        ProgressPercentage = progressPercentage
                    };

                    return chartData;
                }

                return new ChartDataDTO();

            }
            catch (Exception e)
            {
                throw new Exception("No data by given filters");
            }

        }
    }
}
