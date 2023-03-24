using HB.CqrsJwtApp.Core.Application.Dto;
using HB.CqrsJwtApp.Core.Application.Features.CQRS.Queries;
using HB.CqrsJwtApp.Core.Application.Interfaces;
using HB.CqrsJwtApp.Core.Domain;
using MediatR;

namespace HB.CqrsJwtApp.Core.Application.Features.CQRS.Handlers
{
    public class CheckUserQueryRequestHandler : IRequestHandler<CheckUserQueryRequest, CheckUserResponseDto>
    {
        private readonly IRepository<AppUser> userRepository;
        private readonly IRepository<AppRole> roleRepository;

        public CheckUserQueryRequestHandler(IRepository<AppUser> userRepository, IRepository<AppRole> roleRepository)
        {
            this.userRepository = userRepository;
            this.roleRepository = roleRepository;
        }

        public async Task<CheckUserResponseDto> Handle(CheckUserQueryRequest request, CancellationToken cancellationToken)
        {

            var dto = new CheckUserResponseDto();

            var user = await userRepository.GetByFilterAsync(x => x.UserName == request.UserName && x.Password == request.Password);

            if (user==null)
            {
                dto.IsExist = false;
                return dto;
            }

            dto.UserName = user.UserName;
            dto.Id = user.Id;
            dto.IsExist = true;

            var role = await roleRepository.GetByFilterAsync(x => x.Id == user.AppRoleId);

            dto.Role = role?.Definition;

            return dto; 
        }
    }
}
