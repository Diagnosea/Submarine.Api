using System.Threading;
using System.Threading.Tasks;
using Diagnosea.Submarine.Domain.Authentication.Dtos;

namespace Diagnosea.Submarine.Domain.Instructors.Authentication
{
    /// <summary>
    /// Defines behavior for the authentication controller.
    /// </summary>
    public interface IAuthenticationInstructor
    {
        Task<RegisteredDto> RegisterAsync(RegisterDto register, CancellationToken token);
        Task<AuthenticatedDto> AuthenticateAsync(AuthenticateDto authenticate, CancellationToken token);
    }
}