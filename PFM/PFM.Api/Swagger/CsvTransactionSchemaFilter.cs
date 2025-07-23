using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using PFM.Application.UseCases.Transaction.Commands.Import;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace PFM.Api.Swagger
{
    public class CsvTransactionSchemaFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (!string.Equals(operation.OperationId, "Transactions_Import", StringComparison.OrdinalIgnoreCase))
                return;

            var schemaRef = new OpenApiSchema
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.Schema,
                    Id = nameof(TransactionCsv)
                }
            };

            foreach (var media in new[] {"application/csv" })
            {
                if (operation.RequestBody?.Content.TryGetValue(media, out var mt) == true)
                {
                    mt.Schema = schemaRef;
                    mt.Example = new OpenApiString(
@"id,beneficiary-name,date,direction,amount,description,currency,mcc,kind
66229487,Faculty of Arts,1/1/2021,d,187.20,Tuition,USD,8299,pmt
15122088,Glovo,1/1/2021,d,44.30,Food delivery,USD,5811,pmt"
                    );
                }
            }
        }
    }
}
