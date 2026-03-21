using Bookkeeping.Contracts.Common.Results;
using MediatR;

namespace Bookkeeping.Application.Commands.Base.DeleteBase
{
    public record DeleteBaseCommand<TEntity>(Guid Id)
        : IRequest<Result>;
}
