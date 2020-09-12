using Diagnosea.Submarine.Domain.User.Dtos;

namespace Diagnosea.Submarine.Api.Abstractions.Extensions
{
    public static class UserDtoExtensions
    {
        public static UserDto ToResponse(this UserDto user)
        {
            return new UserDto
            {
                Id = user.Id
            };
        }
    }
}