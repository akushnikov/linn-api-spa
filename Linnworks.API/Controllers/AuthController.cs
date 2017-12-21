using System;
using Linnworks.API.Base;
using Linnworks.Client.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Linnworks.API.Controllers
{
    public class AuthController : ApiController
    {
        public AuthController(ILogger<AuthController> logger, ApiClientFactory factory)
            : base(logger)
        {
            Factory = factory;
        }
        
        private ApiClientFactory Factory { get; }

        [HttpGet("validate/{token}"), AllowAnonymous]
        public IActionResult Validate(string token)
        {
            var client = Factory.GetApiClient(token);
            var number = client.GetNewItemNumber();
            return Ok(new {success = true, data = number});
        }
    }
}