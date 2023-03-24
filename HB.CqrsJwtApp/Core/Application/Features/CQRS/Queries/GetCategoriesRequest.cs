using HB.CqrsJwtApp.Core.Application.Dto;
using MediatR;

namespace HB.CqrsJwtApp.Core.Application.Features.CQRS.Queries
{
    public class GetCategoriesQueryRequest:IRequest<List<CategoryListDto>>
    {
    }
}
