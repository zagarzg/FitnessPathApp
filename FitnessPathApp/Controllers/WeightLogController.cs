using FitnessPathApp.BusinessLayer.Interfaces;
using FitnessPathApp.DomainLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FitnessPathApp.API.Controllers
{
    public class WeightLogController : BaseController
    {
        private readonly IWeightLogService _weightLogService;

        public WeightLogController(IWeightLogService weightLogService)
        {
            _weightLogService = weightLogService;
        }

		[HttpGet]
		public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
		{
			return Ok(await _weightLogService.GetAll(cancellationToken));
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
		{
			return Ok(await _weightLogService.Get(id, cancellationToken));
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] WeightLog log, CancellationToken cancellationToken)
		{
			return Ok(await _weightLogService.Create(log, cancellationToken));
		}

		[HttpPut]
		public async Task<IActionResult> Update([FromBody] WeightLog log, CancellationToken cancellationToken)
		{
			await _weightLogService.Update(log, cancellationToken);
			return Ok();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
		{
			await _weightLogService.Delete(id, cancellationToken);
			return Ok();
		}
	}
}
