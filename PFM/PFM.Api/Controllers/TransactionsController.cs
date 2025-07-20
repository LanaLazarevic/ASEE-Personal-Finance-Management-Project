using MediatR;
using Microsoft.AspNetCore.Mvc;
using PFM.Api.Models;
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
        public async Task<ActionResult<Result>> Import([FromBody] ImportTransactionsCommand command)
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

        [HttpGet]
        public async Task<ActionResult<Result<PagedList<TransactionDto>>>> Get([FromQuery] GetTransactionsQuery query)
        {

            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage);
                return BadRequest(
                    Result<PagedList<TransactionDto>>.BadRequest(
                        string.Join("; ", errors)
                    )
                );
            }

            var op = await _mediator.Send(query);

            if (!op.IsSuccess)
                return StatusCode(
                    StatusCodes.Status503ServiceUnavailable,
                    Result<PagedList<TransactionDto>>.ServiceUnavailable(op.ErrorMessage)
                );

            return Ok(Result<PagedList<TransactionDto>>.Ok(op.Value));
        }
    }
}
