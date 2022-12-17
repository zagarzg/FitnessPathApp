using FitnessPathApp.PersistanceLayer.DTOs;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FitnessPathApp.BusinessLayer.Interfaces
{
    public interface IChartService
    {
        Task<ChartDataDTO> GetDataByExerciseName(string exerciseName, int month, CancellationToken cancellationToken);
    }
}
