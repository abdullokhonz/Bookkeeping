using AutoMapper;
using Bookkeeping.Application.Commands.Base.CreateBase;
using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Contracts.DTOs.Accounts5d.CategoryAccount5dDto;
using Bookkeeping.Entities.Accounts5d;
using Bookkeeping.Services.Interfaces.Accounts5d;
using MediatR;

namespace Bookkeeping.Application.Commands.CategoryAccounts5d.CreateCategoryAccount5d
{
    public class CreateCategoryAccount5dCommandHandler
        : CreateBaseCommandHandler<CategoryAccount5d, CategoryAccount5dCreateDto, CategoryAccount5dTreeDto>,
        IRequestHandler<CreateCategoryAccount5dCommand, Result<CategoryAccount5dTreeDto>>
    {
        public CreateCategoryAccount5dCommandHandler(
            ICategoryAccount5dService service,
            IMapper mapper,
            ILogger<CreateCategoryAccount5dCommandHandler> logger)
            : base(service, mapper, logger)
        {

        }

        public async Task<Result<CategoryAccount5dTreeDto>> Handle(
            CreateCategoryAccount5dCommand request, CancellationToken ct)
        {
            return await base.Handle(request, ct);
        }
    }
}
