using AutoMapper;
using HB.CqrsJwtApp.Core.Application.Dto;
using HB.CqrsJwtApp.Core.Application.Features.CQRS.Queries;
using HB.CqrsJwtApp.Core.Application.Interfaces;
using HB.CqrsJwtApp.Core.Domain;
using MediatR;

namespace HB.CqrsJwtApp.Core.Application.Features.CQRS.Handlers
{
    public class GetProductRequestHandler : IRequestHandler<GetProductQueryRequest, ProductListDto>
    {

        private readonly IRepository<Product> repository;
        private readonly IMapper _mapper;

        public GetProductRequestHandler(IRepository<Product> repository, IMapper mapper)
        {
            this.repository = repository;
            _mapper = mapper;
        }

        public async Task<ProductListDto> Handle(GetProductQueryRequest request, CancellationToken cancellationToken)
        {
            var data = await repository.GetByFilterAsync(x => x.Id == request.Id);

            return _mapper.Map<ProductListDto>(data);
        }
    }
}
