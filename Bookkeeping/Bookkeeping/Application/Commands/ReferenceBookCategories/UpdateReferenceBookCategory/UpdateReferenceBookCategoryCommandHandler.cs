using AutoMapper;
using Bookkeeping.Application.Commands.Base.UpdateBase;
using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Contracts.DTOs.ReferenceBooks.ReferenceBookCategoryDto;
using Bookkeeping.Entities.ReferenceBooks;
using Bookkeeping.Services.Interfaces.ReferenceBooks;
using MediatR;

namespace Bookkeeping.Application.Commands.ReferenceBookCategories.UpdateReferenceBookCategory
{
    public class UpdateReferenceBookCategoryCommandHandler
        : UpdateBaseCommandHandler<ReferenceBookCategory, ReferenceBookCategoryUpdateDto>,
        IRequestHandler<UpdateReferenceBookCategoryCommand, Result>
    {
        public UpdateReferenceBookCategoryCommandHandler(
            IReferenceBookCategoryService service,
            IMapper mapper,
            ILogger<UpdateReferenceBookCategoryCommandHandler> logger)
            : base(service, mapper, logger)
        {

        }

        public async Task<Result> Handle(
            UpdateReferenceBookCategoryCommand request, CancellationToken ct)
        {
            return await base.Handle(request, ct);
        }
    }
}
