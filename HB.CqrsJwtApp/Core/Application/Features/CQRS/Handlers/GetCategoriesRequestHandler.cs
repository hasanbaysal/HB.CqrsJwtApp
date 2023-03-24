using AutoMapper;
using HB.CqrsJwtApp.Core.Application.Dto;
using HB.CqrsJwtApp.Core.Application.Features.CQRS.Queries;
using HB.CqrsJwtApp.Core.Application.Interfaces;
using HB.CqrsJwtApp.Core.Domain;
using MediatR;
using System.Runtime.CompilerServices;

namespace HB.CqrsJwtApp.Core.Application.Features.CQRS.Handlers
{
    public class GetCategoriesRequestHandler : IRequestHandler<GetCategoriesQueryRequest,List<CategoryListDto>>
    {
        private readonly IRepository<Category> repository;
        private readonly IMapper mapper;
        public GetCategoriesRequestHandler(IRepository<Category> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<List<CategoryListDto>> Handle(GetCategoriesQueryRequest request, CancellationToken cancellationToken)
        {
          return mapper.Map<List<CategoryListDto>>(await repository.GetAllAsync());
        }
    }
}
