using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Diagnosea.Submarine.Domain.Tank.Dtos;

namespace Diagnosea.Submarine.Domain.Instructors.Tank
{
    public interface ITankInstructor
    {
        Task<IList<TankListDto>> GetByUserIdAsync(Guid userId, CancellationToken token);
    }
}