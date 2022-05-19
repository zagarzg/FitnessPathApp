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
    public class FoodItemController : BaseController
    {
        private readonly IFoodItemService _foodItemService;

        public FoodItemController(IFoodItemService foodItemService)
        {
            _foodItemService = foodItemService;
        }

		[HttpGet]
		public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
		{
			return Ok(await _foodItemService.GetAll(cancellationToken));
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
		{
			return Ok(await _foodItemService.Get(id, cancellationToken));
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] FoodItem item, CancellationToken cancellationToken)
		{
			return Ok(await _foodItemService.Create(item, cancellationToken));
		}

		[HttpPut]
		public async Task<IActionResult> Update([FromBody] FoodItem item, CancellationToken cancellationToken)
		{
			await _foodItemService.Update(item, cancellationToken);
			return Ok();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
		{
			await _foodItemService.Delete(id, cancellationToken);
			return Ok();
		}
	}
}
