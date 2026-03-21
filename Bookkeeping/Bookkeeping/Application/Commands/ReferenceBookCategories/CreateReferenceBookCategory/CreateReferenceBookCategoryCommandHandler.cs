using AutoMapper;
using Bookkeeping.Application.Commands.Base.CreateBase;
using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Contracts.DTOs.ReferenceBooks.ReferenceBookCategoryDto;
using Bookkeeping.Entities.ReferenceBooks;
using Bookkeeping.Services.Interfaces.ReferenceBooks;
using MediatR;

namespace Bookkeeping.Application.Commands.ReferenceBookCategories.CreateReferenceBookCategory
{
    public class CreateReferenceBookCategoryCommandHandler
        : CreateBaseCommandHandler<ReferenceBookCategory, ReferenceBookCategoryCreateDto, ReferenceBookCategoryGetDto>,
        IRequestHandler<CreateReferenceBookCategoryCommand, Result<ReferenceBookCategoryGetDto>>
    {
        public CreateReferenceBookCategoryCommandHandler(
            IReferenceBookCategoryService service,
            IMapper mapper,
            ILogger<CreateReferenceBookCategoryCommandHandler> logger)
            : base(service, mapper, logger)
        {

        }

        public async Task<Result<ReferenceBookCategoryGetDto>> Handle(
            CreateReferenceBookCategoryCommand request, CancellationToken ct)
        {
            return await base.Handle(request, ct);
        }
    }
}
