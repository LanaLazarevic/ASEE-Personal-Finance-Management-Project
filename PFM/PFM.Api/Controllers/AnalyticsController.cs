using MediatR;
using Microsoft.AspNetCore.Mvc;
using PFM.Api.Validation;
using PFM.Application.Result;
using PFM.Application.UseCases.Analytics.Queries.GetSpendingAnalytics;
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
            var (queryModel, errors) = AnalyticsQueryValidationHelper.ParseAndValidate(Request.Query);

            if (errors.Any() || queryModel == null)
                return BadRequest(new { errors });

            var op = await _mediator.Send(queryModel);
            if (!op.IsSuccess)
            {
                object? error = null;
                if (op.code == 503)
                {
                    error = op.Error!
                    .OfType<ServerError>()
                    .Select(e => new
                    {
                        message = e.Message
                    })
                    .ToList();
                } else if (op.code == 440)
                {
                    error = op.Error!
                    .OfType<BusinessError>()
                    .Select(e => new
                    {
                        problem = e.Problem,
                        message = e.Message,
                        details = e.Details
                    })
                    .First();
                }


                    return StatusCode(op.code, error);

            }

            return Ok(op.Value);
        }
    }
}
