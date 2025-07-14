using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;


namespace PFM.Application.UseCases.Catagories.Commands.Import
{
    public record ImportCategoriesCommand(IEnumerable<CategoryCsv> Records) : IRequest<bool>;
}
