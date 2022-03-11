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
    public class FoodLogController : BaseController
    {
        private readonly IFoodLogService _foodLogService;

        public FoodLogController(IFoodLogService foodLogService)
        {
            _foodLogService = foodLogService;
        }
		[HttpGet]
		public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
		{
			return Ok(await _foodLogService.GetAll(cancellationToken));
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
		{
			return Ok(await _foodLogService.Get(id, cancellationToken));
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] FoodLog log, CancellationToken cancellationToken)
		{
			return Ok(await _foodLogService.Create(log, cancellationToken));
		}

		[HttpPut]
		public async Task<IActionResult> Update([FromBody] FoodLog log, CancellationToken cancellationToken)
		{
			await _foodLogService.Update(log, cancellationToken);
			return Ok();
		}

		[HttpDelete]
		public async Task<IActionResult> Delete([FromBody] Guid id, CancellationToken cancellationToken)
		{
			await _foodLogService.Delete(id, cancellationToken);
			return Ok();
		}
	}
}
