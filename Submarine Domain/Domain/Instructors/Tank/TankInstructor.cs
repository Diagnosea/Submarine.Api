using System;
using System.Threading;
using System.Threading.Tasks;
using Abstractions.Exceptions;
using Diagnosea.Submarine.Domain.Tank.Dtos;
using Diagnosea.Submarine.Domain.Tank.Extensions;
using Diagnosea.Submarine.Domain.Tank.Queries.GetTankByUserId;
using Diagnosea.Submarine.Domain.User.Queries.GetUserById;
using MediatR;

namespace Diagnosea.Submarine.Domain.Instructors.Tank
{
    public class TankInstructor : ITankInstructor
    {
        private readonly IMediator _mediator;

        public TankInstructor(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<TankDto> GetByUserIdAsync(Guid userId, CancellationToken token)
        {
            await ValidateUserExistsAsync(userId, token);
            
            var getTankByUserId = new GetTankByUserIdQueryBuilder()
                .WithUserId(userId)
                .Build();

            var tank = await _mediator.Send(getTankByUserId, token);

            if (tank == null)
            {
                throw new SubmarineEntityNotFoundException(
                    $"No Tank With UserId: '{userId}' Found",
                    TankExceptionMessages.NoTankWithUserId);
            }

            return tank.ToDto();
        } 
        
        private async Task ValidateUserExistsAsync(Guid userId, CancellationToken token)
        {
            var getUserByIdQuery = new GetUserByIdQueryBuilder()
                .WithId(userId)
                .Build();
            
            var user = await _mediator.Send(getUserByIdQuery, token);

            if (user == null)
            {
                throw new SubmarineEntityNotFoundException(
                    $"No User With ID: '{userId}' Found",
                    UserExceptionMessages.UserNotFound);
            }
        }
    }
}