using Bookkeeping.Contracts.Common.Results;
using MediatR;

namespace Bookkeeping.Application.Queries.Base.GetAllBase
{
    public record GetAllBaseQuery<TEntity, TGetDto>
        : IRequest<Result<IEnumerable<TGetDto>>>;
}
