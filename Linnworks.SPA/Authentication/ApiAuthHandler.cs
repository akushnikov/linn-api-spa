using System;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Linnworks.Client.Core;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;

namespace Linnworks.SPA.Authentication
{
    public class ApiAuthHandler : AuthenticationHandler<ApiAuthOptions>
    {
        public ApiAuthHandler(IOptionsMonitor<ApiAuthOptions> options, ILoggerFactory logger, UrlEncoder encoder,
            ISystemClock clock, ApiClientFactory factory)
            : base(options, logger, encoder, clock)
        {
            Factory = factory;
        }
        
        private ApiClientFactory Factory { get; }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.TryGetValue(HeaderNames.Authorization, out var authorization))
            {
                return AuthenticateResult.Fail("Cannot read authorization header.");
            }

            if (authorization.Count != 1)
            {
                return AuthenticateResult.Fail("Cannot extract single authorization header.");
            }

            var client = Factory.GetApiClient();
            try
            {
                await client.GetNewItemNumber();
                
                var token = authorization.Single();

                var identity = new ClaimsIdentity(Options.Scheme);
                identity.AddClaim(new Claim(ClaimTypes.Name, token));
                var ticket = new AuthenticationTicket(new ClaimsPrincipal(identity), Options.Scheme);
            
                return AuthenticateResult.Success(ticket);
            }
            catch (Exception e)
            {
                Logger.LogError(e, "api call error");
                return AuthenticateResult.Fail(e);
            }
        }
    }
}