using Bookkeeping.Contracts.Common.Results;
using MediatR;

namespace Bookkeeping.Application.Commands.Base.SoftDeleteBase
{
    public record SoftDeleteBaseCommand<TEntity>(Guid Id)
        : IRequest<Result>;
}
