namespace Diagnosea.Submarine.Abstraction.Routes.UrlBuilders.Authentication
{
    public class AuthenticateUrlBuilder : SubmarineUrlBuilder
    {
        public AuthenticateUrlBuilder(string version) : base(version)
        {
        }

        public override string ToString()
        {
            _parts.Add(RouteConstants.Authentication.Base);
            _parts.Add(RouteConstants.Authentication.Authenticate);
            
            return GetConcatenatedUrlParts();
        }
    }
}