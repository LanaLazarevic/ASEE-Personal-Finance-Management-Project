using MediatR;
using Microsoft.AspNetCore.Mvc;
using PFM.Api.Validation;
using PFM.Application.UseCases.Analytics.Queries.GetSpendingAnalytics;
using PFM.Application.UseCases.Result;
using Swashbuckle.AspNetCore.Annotations;

namespace PFM.Api.Controllers
{
    [Route("spending-analytics")]
    [ApiController]
    public class AnalyticsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AnalyticsController(IMediator mediator)
            => _mediator = mediator;

        [HttpGet]
        [SwaggerOperation(
            OperationId = "Spendings_Get",
            Summary = "Retrieve spending analytics by category or by subcategories within category",
            Description = "Retrieves spending analytics by category or by subcategories within category"
        )]
        public async Task<IActionResult> Get()
        {
            var (queryModel, validationErrors) = AnalyticsQueryValidationHelper.ParseAndValidate(Request.Query);

            if (validationErrors.Any())
                return BadRequest(validationErrors);

            var op = await _mediator.Send(queryModel);
            if (!op.IsSuccess)
            {
                object? errors = null;
                if (op.code == 503)
                {
                    errors = op.Error!
                    .OfType<ServerError>()
                    .Select(e => new
                    {
                        message = e.Message
                    })
                    .ToList();
                }


                return StatusCode(op.code, errors );

            }

            return Ok(op.Value);
        }
    }
}
