using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using PFM.Application.UseCases.Catagories.Commands.Import;
using PFM.Application.UseCases.Transaction.Commands.Import;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

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
            Console.WriteLine(" CsvInputFormatter aktiviran!");
            string csvContent;

            try
            {
                using (var reader = new StreamReader(context.HttpContext.Request.Body, encoding))
                {
                    csvContent = await reader.ReadToEndAsync();
                    if (string.IsNullOrWhiteSpace(csvContent)                         
                        || Regex.IsMatch(csvContent, @"^\s*%PDF", RegexOptions.IgnoreCase)   
                        || Regex.IsMatch(csvContent, @"^\s*PK", RegexOptions.IgnoreCase)     
                        || Regex.IsMatch(csvContent, @"^\s*\xFF\xD8\xFF")                   
                        || Regex.IsMatch(csvContent, @"^\s*(ÿØÿÛ|Exif)")                    
)
                    {
                        context.ModelState.TryAddModelError(
                            "file",
                            "invalid-format:File is not csv file");
                        return await InputFormatterResult.FailureAsync();
                    }
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

                if (context.ModelType == typeof(ImportCategoriesCommand) ||
                    context.ModelType == typeof(IEnumerable<CategoryCsv>) ||
                    context.ModelType == typeof(List<CategoryCsv>))
                {
                    using var sr = new StringReader(csvContent);
                    using var csv = new CsvReader(sr, config);

                    csv.Read();
                    csv.ReadHeader();
                    var header = csv.HeaderRecord;
                    var expectedHeaders = new[] { "code", "name", "parent-code" };

                    if (!expectedHeaders.All(h => header.Contains(h, StringComparer.OrdinalIgnoreCase)))
                    {
                        context.ModelState.TryAddModelError(
                            "header",
                            "invalid-format: CSV header is not valid. Expected header: code, name, parent-code.");
                        return await InputFormatterResult.FailureAsync();
                    }

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

                    csv.Read();
                    csv.ReadHeader();
                    var header = csv.HeaderRecord;
                    var expectedHeaders = new[] { "id", "beneficiary-name", "date", "direction", "amount", "description", "currency", "mcc", "kind" };

                    if (!expectedHeaders.All(h => header.Contains(h, StringComparer.OrdinalIgnoreCase)))
                    {
                        context.ModelState.TryAddModelError(
                            "header",
                            $"invalid-format: CSV header is not valid. Expected header: {string.Join(", ",expectedHeaders)}");
                        return await InputFormatterResult.FailureAsync();
                    }

                    var records = csv.GetRecords<TransactionCsv>().ToList();

                    if (context.ModelType == typeof(ImportTransactionsCommand))
                    {
                        var cmd = new ImportTransactionsCommand(records);
                        return await InputFormatterResult.SuccessAsync(cmd);
                    }

                    return await InputFormatterResult.SuccessAsync(records);
                }
            }
            catch (HeaderValidationException)
            {
                context.ModelState.TryAddModelError(
                    context.ModelName,
                    $"invalid-format:header validation failed");
                return await InputFormatterResult.FailureAsync();
            }
            catch (TypeConverterException)
            {
                context.ModelState.TryAddModelError(
                    context.ModelName,
                    $"invalid-format:type conversion failed");
                return await InputFormatterResult.FailureAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Source
                    + ex.InnerException);
                context.ModelState.TryAddModelError(
                    context.ModelName,
                    $"invalid-format:invalid CSV format");
                return await InputFormatterResult.FailureAsync();
            }
            
            return await InputFormatterResult.FailureAsync();
        }
    }
}

