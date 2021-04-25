using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace NetCoreLibrary.Web.Controllers
{
    public class LogController : Controller
    {
        /// <summary>
        /// Spesifik bir cateyory name yok ise
        /// </summary>
        private readonly ILogger<HomeController> _logger;

        /// <summary>
        /// Spesifik bir category name varsa
        /// </summary>
        private readonly ILoggerFactory _loggerFactory;

        public LogController(ILogger<HomeController> logger, ILoggerFactory loggerFactory)
        {
            _logger = logger;
            _loggerFactory = loggerFactory;
        }

        [HttpGet]
        public IActionResult SampleLogger()
        {
            _logger.LogTrace("Log trace added.");
            _logger.LogDebug("Log debug added.");
            _logger.LogInformation("Index sayfasına girildi.");
            _logger.LogWarning("Log warning added.");

            _logger.LogCritical("Log criticial added.");
            return View();
        }

        public IActionResult SampleLoggerFactory()
        {
            var logger = _loggerFactory.CreateLogger("LoggerForHomeController");

            logger.LogInformation("Logger factory with log information");

            return View();
        }
    }
}
