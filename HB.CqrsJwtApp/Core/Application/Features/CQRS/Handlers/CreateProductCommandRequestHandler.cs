using HB.CqrsJwtApp.Core.Application.Features.CQRS.Commands;
using HB.CqrsJwtApp.Core.Application.Interfaces;
using HB.CqrsJwtApp.Core.Domain;
using MediatR;
using System.Runtime.CompilerServices;

namespace HB.CqrsJwtApp.Core.Application.Features.CQRS.Handlers
{
    public class CreateProductCommandRequestHandler : IRequestHandler<CreateProductCommandRequest>
    {

        private readonly IRepository<Product> repository;

        public CreateProductCommandRequestHandler(IRepository<Product> repository)
        {
            this.repository = repository;
        }

        public async Task<Unit> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {

            await repository.CreateAsync(new Product()
            {
                CategoryId = request.CategoryId,
                Name = request.Name,
                Stock = request.Stock,
                Price = request.Price,

            });

            return Unit.Value;
        }
    }
}
