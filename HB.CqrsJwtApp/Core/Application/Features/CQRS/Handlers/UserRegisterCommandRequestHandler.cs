using HB.CqrsJwtApp.Core.Application.Enums;
using HB.CqrsJwtApp.Core.Application.Features.CQRS.Commands;
using HB.CqrsJwtApp.Core.Application.Interfaces;
using HB.CqrsJwtApp.Core.Domain;
using MediatR;

namespace HB.CqrsJwtApp.Core.Application.Features.CQRS.Handlers
{
    public class UserRegisterCommandRequestHandler : IRequestHandler<UserRegisterCommandRequest>
    {
        private readonly IRepository<AppUser> userRepo;

        public UserRegisterCommandRequestHandler(IRepository<AppUser> userRepo)
        {
            this.userRepo = userRepo;
        }

        public async Task<Unit> Handle(UserRegisterCommandRequest request, CancellationToken cancellationToken)
        {

            AppUser appUser = new()
            {
                Password = request.Password,
                UserName = request.UserName,
                AppRoleId = (int)RoleType.Member
            };

            await userRepo.CreateAsync(appUser);

            return Unit.Value;
        }
    }
}
