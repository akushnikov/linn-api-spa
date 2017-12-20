using System.Linq;
using System.Net.Http;
using Linnworks.Client.Core;
using Linnworks.Client.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;

namespace Linnworks.SPA.Factories
{
    public class ApiClientFactory
    {
        public ApiClientFactory(IHttpContextAccessor accessor)
        {
            Accessor = accessor;
        }
        
        private IHttpContextAccessor Accessor { get; }
                
        public IApiClient GetApiClient()
        {
            var token = Accessor.HttpContext.Request.Headers[HeaderNames.Authorization].Single();
            return new LinnworksClient(new HttpClient(), token);
        }
    }
}