using System;
using System.Threading;
using System.Threading.Tasks;
using Diagnosea.Submarine.Domain.User.Dtos;

namespace Diagnosea.Submarine.Domain.Instructors.User
{
    /// <summary>
    /// Defines behavior for the UserController.
    /// </summary>
    public interface IUserInstructor
    {
        Task<UserDto> GetAsync(Guid id, CancellationToken token);
    }
}