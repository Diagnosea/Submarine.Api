using Diagnosea.Submarine.Abstractions.Responses;
using Diagnosea.Submarine.Domain.User.Dtos;

namespace Diagnosea.Submarine.Api.Abstractions.Extensions
{
    public static class UserDtoExtensions
    {
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