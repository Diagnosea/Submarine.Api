namespace Diagnosea.Submarine.Domain.Authentication
{
    public static class AuthenticationConstants
    {
        public static class ClaimTypes
        {
            public const string Subject = "sub";
            public const string Name = "name";
            public const string Role = "role";
            public const string Expiration = "exp";
            public const string Audience = "aud";
            public const string Issuer = "iss";
            public const string IssuedAt = "iat";
        }
    }
}