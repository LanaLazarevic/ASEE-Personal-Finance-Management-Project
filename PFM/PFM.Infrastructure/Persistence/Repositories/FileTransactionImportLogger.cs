using Microsoft.Extensions.Configuration;
using PFM.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PFM.Infrastructure.Persistence.Repositories
{
    public class FileTransactionImportLogger : ITransactionImportLogger
    {
        private readonly string _logDirectory;

        public FileTransactionImportLogger(IConfiguration config)
        {
            _logDirectory = config["TransactionImportLog:Directory"]
                            ?? Path.Combine(Directory.GetCurrentDirectory(), "logs");
        }

        public async Task LogSkippedAsync(IEnumerable<string> skippedIds, CancellationToken ct = default)
        {
            if (skippedIds == null || !skippedIds.Any()) return;

            Directory.CreateDirectory(_logDirectory);

            var timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
            var fileName = $"skipped-transactions-{timestamp}.log";
            var filePath = Path.Combine(_logDirectory, fileName);

            await File.WriteAllLinesAsync(filePath, skippedIds, ct);
        }
    }
}
