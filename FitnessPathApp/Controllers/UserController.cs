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
    public class UserController : BaseController
    {
		private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
		public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
		{
			return Ok(await _userService.GetAll(cancellationToken));
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
		{
			return Ok(await _userService.Get(id, cancellationToken));
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] User user, CancellationToken cancellationToken)
		{
			return Ok(await _userService.Create(user, cancellationToken));
		}

		[HttpPut]
		public async Task<IActionResult> Update([FromBody] User user, CancellationToken cancellationToken)
		{
			await _userService.Update(user, cancellationToken);
			return Ok();
		}

		[HttpDelete]
		public async Task<IActionResult> Delete([FromBody] Guid id, CancellationToken cancellationToken)
		{
			await _userService.Delete(id, cancellationToken);
			return Ok();
		}
	}
}
