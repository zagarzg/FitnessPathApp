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
    public class ExerciseController : BaseController
    {
		private readonly IExerciseService _exerciseService;

        public ExerciseController(IExerciseService exerciseService)
        {
            _exerciseService = exerciseService;
        }

        [HttpGet]
		public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
		{
			return Ok(await _exerciseService.GetAll(cancellationToken));
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
		{
			return Ok(await _exerciseService.Get(id, cancellationToken));
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] Exercise exercise, CancellationToken cancellationToken)
		{
			return Ok(await _exerciseService.Create(exercise, cancellationToken));
		}

		[HttpPut]
		public async Task<IActionResult> Update([FromBody] Exercise exercise, CancellationToken cancellationToken)
		{
			await _exerciseService.Update(exercise, cancellationToken);
			return Ok();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
		{
			await _exerciseService.Delete(id, cancellationToken);
			return Ok();
		}
	}
}
