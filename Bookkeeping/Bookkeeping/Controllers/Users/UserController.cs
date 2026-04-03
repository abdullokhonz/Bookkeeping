using Bookkeeping.Application.Commands.Users.CreateUser;
using Bookkeeping.Application.Commands.Users.DeleteUser;
using Bookkeeping.Application.Commands.Users.SoftDeleteUser;
using Bookkeeping.Application.Commands.Users.UpdateUser;
using Bookkeeping.Application.Queries.Users.GetAllUser;
using Bookkeeping.Application.Queries.Users.GetPagedUser;
using Bookkeeping.Application.Queries.Users.GetUserById;
using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Contracts.DTOs.Users;
using Bookkeeping.Contracts.Models;
using Bookkeeping.Controllers.Base;
using Bookkeeping.Entities.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookkeeping.Controllers.Users
{
    [Authorize]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UserController
        : BaseController<
            User,
            UserResponseDto,
            RegisterUserDto,
            UserUpdateDto>
    {
        public UserController(
            IMediator mediator,
            ILogger<UserController> logger)
            : base(mediator, logger)
        {

        }

        protected override IRequest<Result<IEnumerable<UserResponseDto>>> GetAllQuery()
            => new GetAllUserQuery();

        protected override IRequest<Result<UserResponseDto>> GetByIdQuery(Guid id)
            => new GetUserByIdQuery(id);

        protected override IRequest<Result<UserResponseDto>> CreateCommand(RegisterUserDto dto)
            => new CreateUserCommand(dto);

        protected override IRequest<Result> UpdateCommand(Guid id, UserUpdateDto dto)
            => new UpdateUserCommand(id, dto);

        protected override IRequest<Result> SoftDeleteCommand(Guid id)
            => new SoftDeleteUserCommand(id);

        protected override IRequest<Result> DeleteCommand(Guid id)
            => new DeleteUserCommand(id);

        protected override IRequest<Result<PagedList<UserResponseDto>>> GetPagedQuery(int page, int size)
            => new GetPagedUserQuery(page, size);
    }
}
