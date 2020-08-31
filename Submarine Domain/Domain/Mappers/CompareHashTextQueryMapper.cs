using Diagnosea.Submarine.Domain.Authentication.Dtos;
using Diagnosea.Submarine.Domain.Authentication.Queries.CompareHashText;

namespace Diagnosea.Submarine.Domain.Mappers
{
    public static class CompareHashTextQueryMapper
    {
        public static CompareHashTextQuery ToCompareHashTextQuery(this AuthenticationDto authentication, string hash)
        {
            return new CompareHashTextQuery
            {
                Text = authentication.Password,
                Hash = hash
            };
        }
    }
}