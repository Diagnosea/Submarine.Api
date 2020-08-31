using System.Collections.Generic;
using Diagnosea.Submarine.Domain.User.Dtos;
using Diagnosea.Submarine.Domain.User.Entities;
using Diagnosea.Submarine.Domain.User.Enums;

namespace Diagnosea.Submarine.Domain.User.Mappers
{
    public static class UserDtoMapper
    {
        public static UserDto ToDto(this UserEntity user)
        {
            return new UserDto
            {
                Id = user.Id,
                UserName = user.UserName,
                FriendlyName = user.FriendlyName,
                Roles = user.Roles ?? new List<UserRole>()
            };
        }
    }
}