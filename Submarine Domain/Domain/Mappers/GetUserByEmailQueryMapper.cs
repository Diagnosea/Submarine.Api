using Diagnosea.Submarine.Domain.Authentication.Dtos;
using Diagnosea.Submarine.Domain.User.Queries.GetUserByEmail;

namespace Diagnosea.Submarine.Domain.Mappers
{
    public static class GetUserByEmailQueryMapper
    {
        public static GetUserByEmailQuery ToGetUserByEmailQuery(this AuthenticationDto authentication)
        {
            return new GetUserByEmailQuery
            {
                EmailAddress = authentication.EmailAddress
            };
        }
    }
}