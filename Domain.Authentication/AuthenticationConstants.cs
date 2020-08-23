namespace Diagnosea.Submarine.Domain.Authentication
{
    public static class AuthenticationConstants
    {
        public static class ClaimTypes
        {
            public const string UserId = "sub";
            public const string UserName = "name";
            public const string Roles = "role";
            public const string Expiration = "exp";
        }
    }
}