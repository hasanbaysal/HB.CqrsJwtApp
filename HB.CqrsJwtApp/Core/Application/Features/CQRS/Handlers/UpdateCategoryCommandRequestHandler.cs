using HB.CqrsJwtApp.Core.Application.Features.CQRS.Commands;
using HB.CqrsJwtApp.Core.Application.Interfaces;
using HB.CqrsJwtApp.Core.Domain;
using MediatR;

namespace HB.CqrsJwtApp.Core.Application.Features.CQRS.Handlers
{
    public class UpdateCategoryCommandRequestHandler : IRequestHandler<UpdateCategoryCommandRequest>
    {

        private readonly IRepository<Category> repository;

        public UpdateCategoryCommandRequestHandler(IRepository<Category> repository)
        {
            this.repository = repository;
        }

        public async Task<Unit> Handle(UpdateCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            var data = await repository.GetByIdAsync(request.Id);


            if (data != null)
            {
                data.Definition=request.Definition;

                await repository.UpdateAsync(data);
            }

            

            return Unit.Value;
        }
    }
}
