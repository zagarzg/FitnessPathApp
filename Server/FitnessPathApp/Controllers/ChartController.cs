using FitnessPathApp.BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace FitnessPathApp.API.Controllers
{
    public class ChartController : BaseController
    {
        private readonly IChartService _chartService;

        public ChartController(IChartService chartService)
        {
            _chartService = chartService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMonthlyChartDataByExerciseName([FromQuery] string exerciseName, [FromQuery] int monthSelected, [FromQuery] int yearSelected, CancellationToken cancellationToken)
        {
            return Ok(await _chartService.GetMonthlyDataByExerciseName(exerciseName, monthSelected, yearSelected, cancellationToken));
        }

        [HttpGet]
        public async Task<IActionResult> GetYearlyChartDataByExerciseName([FromQuery] string exerciseName, [FromQuery] int yearSelected, CancellationToken cancellationToken)
        {
            return Ok(await _chartService.GetYearlyDataByExerciseName(exerciseName, yearSelected, cancellationToken));
        }
    }
}
