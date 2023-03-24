using HB.CqrsJwtApp.Core.Application.Features.CQRS.Commands;
using HB.CqrsJwtApp.Core.Application.Features.CQRS.Queries;
using HB.CqrsJwtApp.Infrastructure.Tools;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace HB.CqrsJwtApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator mediator;

        public AuthController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegisterCommandRequest request)
        {
            await mediator.Send(request);
            return Ok();
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(CheckUserQueryRequest request)
        {

            var dto = await mediator.Send(request);

            if (dto.IsExist)
            {
               
                return Created("", JwtTokenGenerator.GenerateToken(dto));
            }


            return BadRequest("kullanıcı adı veya şifre hatalı");
        }
    }
}
