namespace Diagnosea.Submarine.Abstraction.Routes
{
    public static class RouteConstants
    {
        public const string Version1 = "v1";

        public static class Authentication
        {
            public const string Base = "authentication";
            public const string Register = "register";
            public const string Authenticate = "authenticate";
        }
        
        public static class User
        {
            public const string Base = "user";
        }

        public static class Tank
        {
            public const string Base = "tank";
            public const string Me = "me";
        }
    }
}