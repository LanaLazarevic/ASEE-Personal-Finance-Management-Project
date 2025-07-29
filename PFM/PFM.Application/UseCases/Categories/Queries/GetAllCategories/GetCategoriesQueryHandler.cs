using AutoMapper;
using FluentValidation;
using MediatR;
using NPOI.SS.Formula.Functions;
using PFM.Application.Result;
using PFM.Application.UseCases.Categories.Queries.CetAllCategories;
using PFM.Domain.Dtos;
using PFM.Domain.Entities;
using PFM.Domain.Enums;
using PFM.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PFM.Application.UseCases.Catagories.Queries.GetAllCategories
{
    public class GetCategoriesQueryHandler : IRequestHandler<GetCatagoriesQuery, OperationResult<List<CategoryDto>>>
    {
        private readonly IUnitOfWork _uow;

        private readonly IMapper _mapper;

        public GetCategoriesQueryHandler(IUnitOfWork repository, IMapper mapper )
        {
            _uow = repository;
            _mapper = mapper;
        }

        public async Task<OperationResult<List<CategoryDto>>> Handle(GetCatagoriesQuery request, CancellationToken cancellationToken)
        {

            try
            {
                if(string.IsNullOrWhiteSpace(request.ParentCode))
                {
                    var categories = await _uow.Categories.GetAll(null,cancellationToken);
                    var categoryDtos = _mapper.Map<List<CategoryDto>>(categories);
                    return OperationResult<List<CategoryDto>>.Success(categoryDtos, 200);
                }
                else
                {
                    var cats = await _uow.Categories.GetByCodesAsync(new[] { request.ParentCode }, cancellationToken);
                    var cat = cats.SingleOrDefault();
                    Console.WriteLine(JsonSerializer.Serialize(cat));
                    if (cat == null)
                    {
                        BusinessError error = new BusinessError
                        {
                            Problem = "provided-category-does-not-exists",
                            Details = $"Category '{request.ParentCode}' not found.",
                            Message = "The provided category does not exist."
                        };
                        List<BusinessError> errors = new List<BusinessError> { error };
                        return OperationResult<List<CategoryDto>>.Fail(440, errors);

                    }



                    var categories = await _uow.Categories.GetAll(cat.Code,cancellationToken);
                    var categoryDtos = _mapper.Map<List<CategoryDto>>(categories);
                    return OperationResult<List<CategoryDto>>.Success(categoryDtos, 200);
                }
                   
            }
            catch (Exception ex)
            {
                var error = "An error occurred while fetching transactions. The request timed out." + ex.Message;
                var problem = new ServerError()
                {
                    Message = error
                };
                List<ServerError> problems = new List<ServerError> { problem };
                return OperationResult<List<CategoryDto>>.Fail(503, problems);
            }
        }
    }
}
