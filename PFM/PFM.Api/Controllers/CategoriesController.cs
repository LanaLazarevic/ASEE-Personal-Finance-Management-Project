using MediatR;
using Microsoft.AspNetCore.Mvc;
using PFM.Api.Models;
using PFM.Application.UseCases.Catagories.Commands.Import;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace PFM.Api.Controllers
{
    [Route("categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
            => _mediator = mediator;

        [SwaggerOperation(OperationId = "Category_Import",
                  Summary = "Import categories",
                  Description = "Imports categories via CSV")]
        [HttpPost("import")]
        [Consumes("text/csv", "application/csv")]
        public async Task<ActionResult<Result>> Import([FromBody] ImportCategoriesCommand command)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                       .Values
                       .SelectMany(v => v.Errors)
                       .Select(e => e.ErrorMessage);
                return BadRequest(Result.BadRequest(string.Join("; ", errors)));
            }

            var op = await _mediator.Send(command);
            if (!op.IsSuccess)
                return StatusCode(503, Result.ServiceUnavailable(op.ErrorMessage));

            return Ok(Result.Ok("Categories imported successfully"));
        }
    }
}
