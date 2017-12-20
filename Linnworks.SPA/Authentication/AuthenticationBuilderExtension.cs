using System;
using Microsoft.AspNetCore.Authentication;

namespace Linnworks.SPA.Authentication
{
    public static class AuthenticationBuilderExtensions
    {
        public static AuthenticationBuilder AddCustomAuth(this AuthenticationBuilder builder, Action<ApiAuthOptions> configureOptions)
        {
            return builder.AddScheme<ApiAuthOptions, ApiAuthHandler>(ApiAuthOptions.DefaultScheme, configureOptions);
        }
    }
}