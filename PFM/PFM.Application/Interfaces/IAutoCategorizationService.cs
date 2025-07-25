using PFM.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PFM.Application.Interfaces
{
    public interface IAutoCategorizationService
    {
        Task<int> AutoCategorizeTransactionsAsync(List<CategorizationRule> rules, CancellationToken cancellationToken = default);
    }
}
