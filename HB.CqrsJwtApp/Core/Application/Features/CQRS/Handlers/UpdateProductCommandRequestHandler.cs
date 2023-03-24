using HB.CqrsJwtApp.Core.Application.Features.CQRS.Commands;
using HB.CqrsJwtApp.Core.Application.Interfaces;
using HB.CqrsJwtApp.Core.Domain;
using MediatR;

namespace HB.CqrsJwtApp.Core.Application.Features.CQRS.Handlers
{
    public class UpdateProductCommandRequestHandler : IRequestHandler<UpdateProductCommandRequest>
    {
        private readonly IRepository<Product> repository;

        public UpdateProductCommandRequestHandler(IRepository<Product> repository)
        {
            this.repository = repository;
        }

        public async Task<Unit> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {

            var data = await repository.GetByIdAsync(request.Id);

            if (data != null)
            {

                data.CategoryId = request.CategoryId;
                data.Price = request.Price;
                data.Stock = request.Stock;
                data.Name = request.Name;
                await repository.UpdateAsync(data);

            }

            return Unit.Value;
           
        }
    }
}
