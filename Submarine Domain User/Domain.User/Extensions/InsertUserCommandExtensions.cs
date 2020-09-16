using Diagnosea.Submarine.Domain.User.Commands.InsertUser;
using Diagnosea.Submarine.Domain.User.Entities;

namespace Diagnosea.Submarine.Domain.User.Extensions
{
    public static class InsertUserCommandExtensions
    {
        public static UserEntity ToEntity(this InsertUserCommand insertUserCommand)
        {
            return new UserEntity
            {
                Id = insertUserCommand.Id,
                EmailAddress = insertUserCommand.EmailAddress,
                Password = insertUserCommand.Password,
                UserName = insertUserCommand.UserName,
                FriendlyName = insertUserCommand.FriendlyName,
                Roles = insertUserCommand.Roles
            };
        }
    }
}