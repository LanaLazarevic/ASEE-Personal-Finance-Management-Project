using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PFM.Domain.Interfaces
{
    public interface ITransactionImportLogger
    {
        Task LogSkippedAsync(IEnumerable<string> skippedIds, CancellationToken ct = default);
    }
}
