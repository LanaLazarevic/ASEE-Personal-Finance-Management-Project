using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using PFM.Application.UseCases.Catagories.Commands.Import;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace PFM.Api.Swagger
{
    public class CsvSingleSchemaFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
         
            if (!string.Equals(operation.OperationId, "Category_Import",
                               StringComparison.OrdinalIgnoreCase))
                return;

            var schemaRef = new OpenApiSchema
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.Schema,
                    Id = nameof(CategoryCsv)
                }
            };

            foreach (var media in new[] { "application/csv" })
            {
                if (operation.RequestBody.Content.TryGetValue(media, out var mt))
                {
                    mt.Schema = schemaRef;
                    mt.Example = new OpenApiString(
                        @"code,parent-code,name
A,,Misc Expenses
B,,Auto & Transport
C,,Bills & Utilities"
                    );
                }
            }
        }
    }
}
