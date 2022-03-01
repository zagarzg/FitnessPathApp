using FitnessPathApp.BusinessLayer.Interfaces;
using FitnessPathApp.DomainLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FitnessPathApp.API.Controllers
{
    public class TrainingLogController : BaseController
    {
		private readonly ITrainingLogService _trainingLogService;

        public TrainingLogController(ITrainingLogService trainingLogService)
        {
            _trainingLogService = trainingLogService;
        }

        [HttpGet]
		public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
		{
			return Ok(await _trainingLogService.GetAll(cancellationToken));
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
		{
			return Ok(await _trainingLogService.Get(id, cancellationToken));
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] TrainingLog log, CancellationToken cancellationToken)
		{
			return Ok(await _trainingLogService.Create(log, cancellationToken));
		}

		[HttpPut]
		public async Task<IActionResult> Update([FromBody] TrainingLog log, CancellationToken cancellationToken)
		{
			await _trainingLogService.Update(log, cancellationToken);
			return Ok();
		}

		[HttpDelete]
		public async Task<IActionResult> Delete([FromBody] Guid id, CancellationToken cancellationToken)
		{
			await _trainingLogService.Delete(id, cancellationToken);
			return Ok();
		}
	}
}
