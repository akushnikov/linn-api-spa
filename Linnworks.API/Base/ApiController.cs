using System;
using Linnworks.Client.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Linnworks.API.Base
{
    [Route("api/[controller]"), Authorize]
    public abstract class ApiController : Controller
    {
        protected ApiController(ILogger logger)
        {
            Logger = logger;
        }

        protected ILogger Logger { get; }

        protected IActionResult HandleApiException(Exception ex)
        {
            switch (ex)
            {
                case ApiException e:
                    return BadRequest(new {success = false, data = e.Message});
                default:
                    Logger.LogError(ex, "Unknown api call error");
                    return BadRequest(new {success = false, data = ex.Message});
            }
        }
    }
}