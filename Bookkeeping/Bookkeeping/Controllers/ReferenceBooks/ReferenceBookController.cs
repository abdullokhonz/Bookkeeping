using Bookkeeping.Application.Commands.ReferenceBooks.CreateReferenceBook;
using Bookkeeping.Application.Commands.ReferenceBooks.DeleteReferenceBook;
using Bookkeeping.Application.Commands.ReferenceBooks.SoftDeleteReferenceBook;
using Bookkeeping.Application.Commands.ReferenceBooks.UpdateReferenceBook;
using Bookkeeping.Application.Queries.ReferenceBooks.GetAllReferenceBook;
using Bookkeeping.Application.Queries.ReferenceBooks.GetPagedReferenceBook;
using Bookkeeping.Application.Queries.ReferenceBooks.GetReferenceBookById;
using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Contracts.DTOs.ReferenceBooks.ReferenceBookDto;
using Bookkeeping.Contracts.Models;
using Bookkeeping.Controllers.Base;
using Bookkeeping.Entities.ReferenceBooks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookkeeping.Controllers.ReferenceBooks
{
    [Authorize]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ReferenceBookController : BaseController
            <ReferenceBook,
            ReferenceBookGetDto,
            ReferenceBookCreateDto,
            ReferenceBookUpdateDto>
    {
        public ReferenceBookController(
            IMediator mediator,
            ILogger<ReferenceBookController> logger)
            : base(mediator, logger)
        {

        }

        protected override IRequest<Result<IEnumerable<ReferenceBookGetDto>>> GetAllQuery()
            => new GetAllReferenceBookQuery();

        protected override IRequest<Result<ReferenceBookGetDto>> GetByIdQuery(Guid id)
            => new GetReferenceBookByIdQuery(id);

        protected override IRequest<Result<ReferenceBookGetDto>> CreateCommand(ReferenceBookCreateDto dto)
            => new CreateReferenceBookCommand(dto);

        protected override IRequest<Result> UpdateCommand(Guid id, ReferenceBookUpdateDto dto)
            => new UpdateReferenceBookCommand(id, dto);

        protected override IRequest<Result> SoftDeleteCommand(Guid id)
            => new SoftDeleteReferenceBookCommand(id);

        protected override IRequest<Result> DeleteCommand(Guid id)
            => new DeleteReferenceBookCommand(id);

        protected override IRequest<Result<PagedList<ReferenceBookGetDto>>> GetPagedQuery(int page, int size)
            => new GetPagedReferenceBookQuery(page, size);
    }
}
