using MediatR;
using Microsoft.AspNetCore.Mvc;
using PFM.Application.UseCases.Transaction.Commands.Import;
using PFM.Application.UseCases.Transaction.Queries.GetAllTransactions;
using PFM.Domain.Dtos;
using Swashbuckle.AspNetCore.Annotations;

namespace PFM.Api.Controllers
{
    [Route("transactions")]
    [ApiController]
    public class TransactionsController : Controller
    {

        private readonly IMediator _mediator;

        public TransactionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [SwaggerOperation(OperationId = "Transactions_Import", Summary = "Import transactions", Description = "Imports transactions via CSV")]
        [HttpPost("import")]
        [Consumes("text/csv", "application/csv")]
        public async Task<IActionResult> Import([FromBody] ImportTransactionsCommand command)
        {
            try
            {
                await _mediator.Send(command);
                return Ok(new { message = "Transactions imported." });
            }
            catch (ApplicationException appEx)
            {
                return StatusCode(503, new { error = appEx.Message });
            }
        }

        [HttpGet]
        public async Task<ActionResult<GetAllTransactionsResponse>> Get([FromQuery] GetTransactionsQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
