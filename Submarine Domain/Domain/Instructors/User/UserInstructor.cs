using System;
using System.Threading;
using System.Threading.Tasks;
using Diagnosea.Submarine.Domain.User.Dtos;
using Diagnosea.Submarine.Domain.User.Mappers;
using Diagnosea.Submarine.Domain.User.Queries.GetUserById;
using MediatR;

namespace Diagnosea.Submarine.Domain.Instructors.User
{
    public class UserInstructor : IUserInstructor
    {
        private IMediator _mediator;

        public UserInstructor(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<UserDto> GetAsync(Guid id, CancellationToken token)
        {
            var getUserByIdQuery = new GetUserByIdQueryBuilder()
                .WithId(id)
                .Build();
            
            var user = await _mediator.Send(getUserByIdQuery, token);

            return user.ToDto();
        }
    }
}