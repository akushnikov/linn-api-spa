using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Linnworks.API.Base
{
    [Route("api/[controller]")]
    public abstract class ApiController : Controller
    {
        protected ApiController(ILogger logger)
        {
            Logger = logger;
        }
        
        protected ILogger Logger { get; }
    }
}