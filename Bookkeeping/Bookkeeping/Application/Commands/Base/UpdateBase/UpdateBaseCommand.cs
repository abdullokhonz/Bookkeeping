using Bookkeeping.Contracts.Common.Results;
using MediatR;

namespace Bookkeeping.Application.Commands.Base.UpdateBase
{
    public record UpdateBaseCommand<TEntity, TUpdateDto>(Guid Id, TUpdateDto Dto)
        : IRequest<Result>;
}
