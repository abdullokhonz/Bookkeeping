using Bookkeeping.Contracts.Common.Results;
using MediatR;

namespace Bookkeeping.Application.Commands.Base.RemoveTreeBase
{
    public record RemoveTreeBaseCommand<TEntity>(Guid Id)
        : IRequest<Result>;
}
