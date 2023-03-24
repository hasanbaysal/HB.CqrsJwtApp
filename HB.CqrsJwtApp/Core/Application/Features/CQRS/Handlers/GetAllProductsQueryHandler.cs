using AutoMapper;
using HB.CqrsJwtApp.Core.Application.Dto;
using HB.CqrsJwtApp.Core.Application.Features.CQRS.Queries;
using HB.CqrsJwtApp.Core.Application.Interfaces;
using HB.CqrsJwtApp.Core.Domain;
using MediatR;

namespace HB.CqrsJwtApp.Core.Application.Features.CQRS.Handlers
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQueryRequest, List<ProductListDto>>
    {
        private readonly IRepository<Product>  repository;
        private readonly IMapper _mapper;

        public GetAllProductsQueryHandler(Interfaces.IRepository<Product> repository, IMapper mapper)
        {
            this.repository = repository;
            _mapper = mapper;
        }

        public async Task<List<ProductListDto>> Handle(GetAllProductsQueryRequest request, CancellationToken cancellationToken)
        {

            return _mapper.Map<List<ProductListDto>>(await this.repository.GetAllAsync());
            
        }
    }
}
