using HB.CqrsJwtApp.Core.Application.Features.CQRS.Commands;
using HB.CqrsJwtApp.Core.Application.Interfaces;
using HB.CqrsJwtApp.Core.Domain;
using HB.CqrsJwtApp.Persistance.Repositories;
using MediatR;

namespace HB.CqrsJwtApp.Core.Application.Features.CQRS.Handlers
{
    public class DeleteCategoryCommandRequestHandler : IRequestHandler<DeleteCategoryCommandRequest>
    {

        private readonly IRepository<Category> repository;

        public DeleteCategoryCommandRequestHandler(IRepository<Category> repository)
        {
            this.repository = repository;
        }

        public async Task<Unit> Handle(DeleteCategoryCommandRequest request, CancellationToken cancellationToken)
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
