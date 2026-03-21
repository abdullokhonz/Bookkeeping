using Bookkeeping.Contracts.Common.Results;
using MediatR;

namespace Bookkeeping.Application.Queries.Base.GetBaseById
{
    public record GetBaseByIdQuery<TEntity, TGetDto>(Guid Id)
        : IRequest<Result<TGetDto>>;
}
