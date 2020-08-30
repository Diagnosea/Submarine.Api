using Diagnosea.Submarine.Domain.User.Commands.InsertUser;
using Diagnosea.Submarine.Domain.User.Entities;

namespace Diagnosea.Submarine.Domain.User.Mappers
{
    public static class UserEntityMapper
    {
        internal static UserEntity ToEntity(this InsertUserCommand insertUserCommand)
        {
            return new UserEntity
            {
                Id = insertUserCommand.Id,
                EmailAddress = insertUserCommand.EmailAddress,
                Password = insertUserCommand.Password,
                UserName = insertUserCommand.UserName,
                Roles = insertUserCommand.Roles
            };
        }
    }
}