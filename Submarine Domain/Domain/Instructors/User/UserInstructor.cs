using System;
using System.Threading;
using System.Threading.Tasks;
using Abstractions.Exceptions;
using Diagnosea.Submarine.Domain.User.Dtos;
using Diagnosea.Submarine.Domain.User.Extensions;
using Diagnosea.Submarine.Domain.User.Queries.GetUserById;
using MediatR;

namespace Diagnosea.Submarine.Domain.Instructors.User
{
    public class UserInstructor : IUserInstructor
    {
        private readonly IMediator _mediator;

        public UserInstructor(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<UserDto> GetAsync(Guid userId, CancellationToken token)
        {
            var getUserByIdQuery = new GetUserByIdQueryBuilder()
                .WithId(userId)
                .Build();
            
            var user = await _mediator.Send(getUserByIdQuery, token);

            if (user == null)
            {
                throw new EntityNotFoundException(
                    $"No User With ID: '{userId}' Found",
                    ExceptionMessages.User.UserNotFound);
            }

            return user.ToDto();
        }
    }
}