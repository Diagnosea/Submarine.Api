using System;
using System.Threading;
using System.Threading.Tasks;
using Diagnosea.Submarine.Domain.Tank.Dtos;

namespace Diagnosea.Submarine.Domain.Instructors.Tank
{
    public interface ITankInstructor
    {
        Task<TankDto> GetByUserIdAsync(Guid userId, CancellationToken token);
    }
}