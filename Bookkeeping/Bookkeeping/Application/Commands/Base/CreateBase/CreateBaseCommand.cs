using Bookkeeping.Contracts.Common.Results;
using MediatR;

namespace Bookkeeping.Application.Commands.Base.CreateBase
{
    public record CreateBaseCommand<TEntity, TCreateDto, TGetDto>(TCreateDto Dto)
        : IRequest<Result<TGetDto>>;
}
