using Diagnosea.Submarine.Domain.User.Dtos;
using Diagnosea.Submarine.Domain.User.Entities;

namespace Diagnosea.Submarine.Domain.User.Extensions
{
    public static class UserEntityExtensions
    {
        public static UserDto ToDto(this UserEntity user)
        {
            return new UserDto
            {
                Id = user.Id,
                UserName = user.UserName,
                FriendlyName = user.FriendlyName
            };
        }
    }
}