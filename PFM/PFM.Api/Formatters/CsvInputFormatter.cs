using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using PFM.Application.UseCases.Catagories.Commands.Import;
using PFM.Application.UseCases.Transaction.Commands.Import;
using System.Globalization;
using System.Text;

namespace PFM.Api.Formatters
{
    public class CsvInputFormatter : TextInputFormatter
    {
        public CsvInputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/csv"));
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("application/csv"));
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        protected override bool CanReadType(Type type)
        {
            return type == typeof(ImportCategoriesCommand)
                || type == typeof(IEnumerable<CategoryCsv>)
                || type == typeof(List<CategoryCsv>)
                || type == typeof(ImportTransactionsCommand)
                || type == typeof(IEnumerable<TransactionCsv>) 
                || type == typeof(List<TransactionCsv>);
        }

        public override async Task<InputFormatterResult> ReadRequestBodyAsync(
            InputFormatterContext context, Encoding encoding)
        {
            string csvContent;
            using (var reader = new StreamReader(context.HttpContext.Request.Body, encoding))
            {
                csvContent = await reader.ReadToEndAsync();
            }

            using var stringReader = new StringReader(csvContent);
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ",",
                HasHeaderRecord = true,
                BadDataFound = null,
                MissingFieldFound = null,
                HeaderValidated = null    
            };

            try
            {
                if (context.ModelType == typeof(ImportCategoriesCommand) ||
                    context.ModelType == typeof(IEnumerable<CategoryCsv>) ||
                    context.ModelType == typeof(List<CategoryCsv>))
                {
                    using var sr = new StringReader(csvContent);
                    using var csv = new CsvReader(sr, config);
                    var records = csv.GetRecords<CategoryCsv>().ToList();

                    if (context.ModelType == typeof(ImportCategoriesCommand))
                    {
                        var cmd = new ImportCategoriesCommand(records);
                        return await InputFormatterResult.SuccessAsync(cmd);
                    }

                    return await InputFormatterResult.SuccessAsync(records);
                }

                else if (context.ModelType == typeof(ImportTransactionsCommand) ||
                         context.ModelType == typeof(IEnumerable<TransactionCsv>) ||
                         context.ModelType == typeof(List<TransactionCsv>))
                {
                    using var sr = new StringReader(csvContent);
                    using var csv = new CsvReader(sr, config);
                    var records = csv.GetRecords<TransactionCsv>().ToList();

                    if (context.ModelType == typeof(ImportTransactionsCommand))
                    {
                        var cmd = new ImportTransactionsCommand(records);
                        return await InputFormatterResult.SuccessAsync(cmd);
                    }

                    return await InputFormatterResult.SuccessAsync(records);
                }
            }
            catch (Exception ex)
            {

                context.ModelState.TryAddModelError(
                    context.ModelName,
                    "Invalid CSV format: " + ex.Message);
                return await InputFormatterResult.FailureAsync();
            }

            return await InputFormatterResult.FailureAsync();
        }
    }
}

