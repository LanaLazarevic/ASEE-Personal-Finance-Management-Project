using MediatR;
using PFM.Application.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PFM.Application.UseCases.Catagories.Commands.Import
{
    public record ImportCategoriesCommand(IEnumerable<CategoryCsv> Records) : IRequest<OperationResult>;
}
