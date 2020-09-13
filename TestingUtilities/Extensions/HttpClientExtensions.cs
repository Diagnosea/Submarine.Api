using System.Net.Http;
using System.Net.Http.Headers;
using Diagnosea.Submarine.Domain.Authentication;

namespace Diagnosea.Submarine.TestingUtilities.Extensions
{
    public static class HttpClientExtensions
    {
        public static void SetBearerToken(this HttpClient client, string bearerToken)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(SubmarineSecurityDefinitionNames.Bearer, bearerToken);
        }

        public static void ClearBearerToken(this HttpClient client)
        {
            client.DefaultRequestHeaders.Authorization = null;
        }
    }
}