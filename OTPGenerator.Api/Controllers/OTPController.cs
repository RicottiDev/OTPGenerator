using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OTPGenerator.Core.Interfaces.IServices;

namespace OTPGenerator.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OTPController : ControllerBase
    {
        private readonly ILogger<OTPController> _logger;
        private readonly IOTPService _OTPService;

        public OTPController(ILogger<OTPController> logger,
                             IOTPService jobService)
        {
            _logger = logger;
            _OTPService = jobService;
        }
    }
}
