using HB.CqrsJwtApp.Core.Application.Features.CQRS.Commands;
using HB.CqrsJwtApp.Core.Application.Features.CQRS.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HB.CqrsJwtApp.Controllers
{
    [Authorize(Roles ="Admin, Member")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var data = await _mediator.Send(new GetAllProductsQueryRequest());
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var data = await _mediator.Send(new GetProductQueryRequest(id));

            return data == null ? NotFound(): Ok(data); 
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
           var result = await _mediator.Send(new DeleteProductCommandRequest(id));


            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Create (CreateProductCommandRequest request)
        {
            await _mediator.Send(request);
            return Created("", request);
        }
        [HttpPut]
        public async Task<IActionResult> Update(UpdateProductCommandRequest request)
        {
            await _mediator.Send(request);
            return Ok();
        }

    }
}
