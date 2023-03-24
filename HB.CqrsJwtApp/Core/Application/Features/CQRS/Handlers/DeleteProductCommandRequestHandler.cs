using HB.CqrsJwtApp.Core.Application.Features.CQRS.Commands;
using HB.CqrsJwtApp.Core.Application.Interfaces;
using HB.CqrsJwtApp.Core.Domain;
using MediatR;
using System.Runtime.CompilerServices;

namespace HB.CqrsJwtApp.Core.Application.Features.CQRS.Handlers
{
    public class DeleteProductCommandRequestHandler : IRequestHandler<DeleteProductCommandRequest>
    {
        private readonly IRepository<Product> repository;

        public DeleteProductCommandRequestHandler(IRepository<Product> repository)
        {
            this.repository = repository;
        }

        public async Task<Unit> Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
        {

            var data = await repository.GetByIdAsync(request.Id);


            if (data != null)
            {
                await repository.RemoveAsync(data);
            }

            return Unit.Value;
        }
    }
}
