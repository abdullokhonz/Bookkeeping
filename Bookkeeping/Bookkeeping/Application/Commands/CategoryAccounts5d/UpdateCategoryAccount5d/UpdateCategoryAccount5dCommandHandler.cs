using AutoMapper;
using Bookkeeping.Application.Commands.Base.UpdateBase;
using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Contracts.DTOs.Accounts5d.CategoryAccount5dDto;
using Bookkeeping.Entities.Accounts5d;
using Bookkeeping.Services.Interfaces.Accounts5d;
using MediatR;

namespace Bookkeeping.Application.Commands.CategoryAccounts5d.UpdateCategoryAccount5d
{
    public class UpdateCategoryAccount5dCommandHandler
        : UpdateBaseCommandHandler<CategoryAccount5d, CategoryAccount5dUpdateDto>,
        IRequestHandler<UpdateCategoryAccount5dCommand, Result>
    {
        public UpdateCategoryAccount5dCommandHandler(
            ICategoryAccount5dService service,
            IMapper mapper,
            ILogger<UpdateCategoryAccount5dCommandHandler> logger)
            : base(service, mapper, logger)
        {

        }

        public async Task<Result> Handle(
            UpdateCategoryAccount5dCommand request, CancellationToken ct)
        {
            return await base.Handle(request, ct);
        }
    }
}
