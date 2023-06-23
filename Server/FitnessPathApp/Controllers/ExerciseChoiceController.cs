using FitnessPathApp.BusinessLayer.Interfaces;
using FitnessPathApp.DomainLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FitnessPathApp.API.Controllers
{
    public class ExerciseChoiceController : BaseController
    {
        private readonly IExerciseChoiceService _exerciseChoiceService;

        public ExerciseChoiceController(IExerciseChoiceService exerciseChoiceService)
        {
            _exerciseChoiceService = exerciseChoiceService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            return Ok(await _exerciseChoiceService.GetAll(cancellationToken));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
        {
            return Ok(await _exerciseChoiceService.Get(id, cancellationToken));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ExerciseChoice exerciseChoice, CancellationToken cancellationToken)
        {
            return Ok(await _exerciseChoiceService.Create(exerciseChoice, cancellationToken));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ExerciseChoice exerciseChoice, CancellationToken cancellationToken)
        {
            await _exerciseChoiceService.Update(exerciseChoice, cancellationToken);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await _exerciseChoiceService.Delete(id, cancellationToken);
            return Ok();
        }
    }
}
