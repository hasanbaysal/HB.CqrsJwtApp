using HB.CqrsJwtApp.Core.Application.Dto;
using MediatR;

namespace HB.CqrsJwtApp.Core.Application.Features.CQRS.Queries
{
    public class GetProductQueryRequest:IRequest<ProductListDto>
    {
        public int Id{ get; set; }

        public GetProductQueryRequest(int ıd)
        {
            Id = ıd;
        }
    }
}
