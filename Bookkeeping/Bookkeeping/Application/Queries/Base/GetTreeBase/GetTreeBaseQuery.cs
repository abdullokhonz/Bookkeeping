using Bookkeeping.Contracts.Common.Results;
using MediatR;

namespace Bookkeeping.Application.Queries.Base.GetTreeBase
{
    public record GetTreeBaseQuery<TEntity, TResponse>
        : IRequest<Result<IEnumerable<TResponse>>>;
}
