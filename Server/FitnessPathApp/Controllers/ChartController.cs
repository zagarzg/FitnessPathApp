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
        public async Task<IActionResult> GetChartDataByExerciseName([FromQuery] string exerciseName, [FromQuery] int monthSelected, CancellationToken cancellationToken)
        {
            return Ok(await _chartService.GetDataByExerciseName(exerciseName, monthSelected, cancellationToken));
        }
    }
}
