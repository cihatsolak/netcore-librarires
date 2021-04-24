using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace NetCoreLibrary.Web.Controllers
{
    public class LogController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public LogController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            _logger.LogTrace("Log trace added.");
            _logger.LogDebug("Log debug added.");
            _logger.LogInformation("Index sayfasına girildi.");
            _logger.LogWarning("Log warning added.");

            _logger.LogCritical("Log criticial added.");
            return View();
        }
    }
}
