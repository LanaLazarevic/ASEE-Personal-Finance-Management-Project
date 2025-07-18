using MediatR;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> Import([FromBody] ImportCategoriesCommand command)
        {

            try
            {
                await _mediator.Send(command);
                return Ok(new { message = "Categories imported." });
            }
            catch (ApplicationException appEx)
            {
                return StatusCode(503, new { error = appEx.Message });
            }
        }
    }
}
