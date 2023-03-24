using MediatR;

namespace HB.CqrsJwtApp.Core.Application.Features.CQRS.Commands
{
    public class UserRegisterCommandRequest:IRequest
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
}
