using System.Net.Http;
using System.Net.Http.Headers;

namespace Diagnosea.Submarine.Utilities.Extensions
{
    public static class HttpClientExtensions
    {
        public static void SetBearerToken(this HttpClient client, string bearerToken)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
        }

        public static void ClearBearerToken(this HttpClient client)
        {
            client.DefaultRequestHeaders.Authorization = null;
        }
    }
}