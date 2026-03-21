using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Contracts.Models;
using MediatR;

namespace Bookkeeping.Application.Queries.Base.GetPagedBase
{
    public record GetPagedBaseQuery<TEntity, TResponse>(int Page, int Size)
        : IRequest<Result<PagedList<TResponse>>>;
}
