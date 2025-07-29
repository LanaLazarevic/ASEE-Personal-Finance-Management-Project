using MediatR;
using Microsoft.Extensions.Configuration;
using PFM.Application.Interfaces;
using PFM.Application.Result;
using PFM.Application.UseCases.Transaction.Commands.AutoCategorization;
using PFM.Domain.Dtos;

namespace PFM.Application.UseCases.Transaction.Commands.AutoCategorize
{
    public class AutoCategorizeTransactionsHandler : IRequestHandler<AutoCategorizeTransactionsCommand, OperationResult>
    {
        private readonly IAutoCategorizationService _service;
        private readonly IConfiguration _configuration;

        public AutoCategorizeTransactionsHandler(IAutoCategorizationService service, IConfiguration configuration)
        {
            _service = service;
            _configuration = configuration;
        }

        public async Task<OperationResult> Handle(AutoCategorizeTransactionsCommand request, CancellationToken cancellationToken)
        {
            var rules = _configuration
            .GetSection("CategorizationRules:rules")
            .Get<List<CategorizationRule>>();

            if (rules == null || rules.Count == 0)
            {
                BusinessError error = new BusinessError
                {
                    Problem = "no-rules",
                    Details = $"No rules found in configuration",
                    Message = "No rules found"
                };
                List<BusinessError> errors = new List<BusinessError> { error };
                return OperationResult.Fail(440, errors);
            }
                

            var br = await _service.AutoCategorizeTransactionsAsync(rules, cancellationToken);
            if(br == -1)
            {
                BusinessError error = new BusinessError
                {
                    Problem = "invalid-rule",
                    Details = $"Invalid rule found please check config file",
                    Message = "Invalid rule found"
                };
                List<BusinessError> errors = new List<BusinessError> { error };
                return OperationResult.Fail(440, errors);
            }
            return OperationResult.Success();
        }
    }
}
