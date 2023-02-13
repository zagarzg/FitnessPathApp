using FitnessPathApp.PersistanceLayer.DTOs;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FitnessPathApp.BusinessLayer.Interfaces
{
    public interface IChartService
    {
        Task<ChartDataDTO> GetMonthlyDataByExerciseName(string exerciseName, int month, int year, CancellationToken cancellationToken);
        Task<ChartDataDTO> GetYearlyDataByExerciseName(string exerciseName, int year, CancellationToken cancellationToken);

        Task<ChartDataDTO> GetMonthlyWeightChangeData(int month, int year, CancellationToken cancellationToken);
        Task<ChartDataDTO> GetYearlyWeightChangeData(int year, CancellationToken cancellationToken);
    }
}
