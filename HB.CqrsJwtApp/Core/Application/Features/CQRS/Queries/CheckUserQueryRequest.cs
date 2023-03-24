using HB.CqrsJwtApp.Core.Application.Dto;
using MediatR;

namespace HB.CqrsJwtApp.Core.Application.Features.CQRS.Queries
{
    public class CheckUserQueryRequest:IRequest<CheckUserResponseDto>
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
}
