using AutoMapper;
using HB.CqrsJwtApp.Core.Application.Dto;
using HB.CqrsJwtApp.Core.Application.Features.CQRS.Queries;
using HB.CqrsJwtApp.Core.Application.Interfaces;
using HB.CqrsJwtApp.Core.Domain;
using MediatR;

namespace HB.CqrsJwtApp.Core.Application.Features.CQRS.Handlers
{
    public class GetCategoryQueryRequestHandler : IRequestHandler<GetCategoryQueryRequest, CategoryListDto>
    {
        private readonly IRepository<Category> repository;
        private readonly IMapper mapper;

        public GetCategoryQueryRequestHandler(IRepository<Category> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<CategoryListDto> Handle(GetCategoryQueryRequest request, CancellationToken cancellationToken)
        {

            return mapper.Map<CategoryListDto>(await repository.GetByIdAsync(request.Id));
            
        }
    }
}
