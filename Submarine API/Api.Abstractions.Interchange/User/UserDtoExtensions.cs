using Diagnosea.Submarine.Abstractions.Interchange.Responses.User;
using Diagnosea.Submarine.Domain.User.Dtos;

namespace Diagnosea.Submarine.Api.Abstractions.Interchange.User
{
    public static class UserDtoExtensions
    {
        // TODO: Unit tests.
        public static UserResponse ToResponse(this UserDto user)
        {
            return new UserResponse
            {
                Id = user.Id,
                UserName = user.UserName,
                FriendlyName = user.FriendlyName
            };
        }
    }
}