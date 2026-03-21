using Bookkeeping.Application.Commands.IfrsAccounts.CreateIfrsAccount;
using Bookkeeping.Application.Commands.IfrsAccounts.DeleteIfrsAccount;
using Bookkeeping.Application.Commands.IfrsAccounts.RemoveTreeIfrsAccount;
using Bookkeeping.Application.Commands.IfrsAccounts.SoftDeleteIfrsAccount;
using Bookkeeping.Application.Commands.IfrsAccounts.UpdateIfrsAccount;
using Bookkeeping.Application.Queries.IfrsAccounts.GetAllIfrsAccount;
using Bookkeeping.Application.Queries.IfrsAccounts.GetIfrsAccountById;
using Bookkeeping.Application.Queries.IfrsAccounts.GetPagedIfrsAccount;
using Bookkeeping.Application.Queries.IfrsAccounts.GetTreeIfrsAccount;
using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Contracts.DTOs.Accounts5d.IfrsAccountDto;
using Bookkeeping.Contracts.Models;
using Bookkeeping.Controllers.Base;
using Bookkeeping.Entities.Accounts5d;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bookkeeping.Controllers.Accounts5d
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class IfrsAccountController : TreeBaseController
            <IfrsAccount,
            IfrsAccountTreeDto,
            IfrsAccountCreateDto,
            IfrsAccountUpdateDto>
    {
        public IfrsAccountController(
            IMediator mediator,
            ILogger<IfrsAccountController> logger)
            : base(mediator, logger)
        {

        }

        protected override IRequest<Result<IEnumerable<IfrsAccountTreeDto>>> GetAllQuery()
            => new GetAllIfrsAccountQuery();

        protected override IRequest<Result<IfrsAccountTreeDto>> GetByIdQuery(Guid id)
            => new GetIfrsAccountByIdQuery(id);

        protected override IRequest<Result<IfrsAccountTreeDto>> CreateCommand(IfrsAccountCreateDto dto)
            => new CreateIfrsAccountCommand(dto);

        protected override IRequest<Result> UpdateCommand(Guid id, IfrsAccountUpdateDto dto)
            => new UpdateIfrsAccountCommand(id, dto);

        protected override IRequest<Result> SoftDeleteCommand(Guid id)
            => new SoftDeleteIfrsAccountCommand(id);

        protected override IRequest<Result> DeleteCommand(Guid id)
            => new DeleteIfrsAccountCommand(id);

        protected override IRequest<Result<PagedList<IfrsAccountTreeDto>>> GetPagedQuery(int page, int size)
            => new GetPagedIfrsAccountQuery(page, size);

        protected override IRequest<Result<IEnumerable<IfrsAccountTreeDto>>> GetTreeQuery()
            => new GetTreeIfrsAccountQuery();

        protected override IRequest<Result> RemoveTreeCommand(Guid id)
            => new RemoveTreeIfrsAccountCommand(id);
    }
}
