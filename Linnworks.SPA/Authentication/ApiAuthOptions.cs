using Microsoft.AspNetCore.Authentication;

namespace Linnworks.SPA.Authentication
{
    public class ApiAuthOptions : AuthenticationSchemeOptions
    {
        public const string DefaultScheme = "api-auth";
        public string Scheme => DefaultScheme;
    }
}