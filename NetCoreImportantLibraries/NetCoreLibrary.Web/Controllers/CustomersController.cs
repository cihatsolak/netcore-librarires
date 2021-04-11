using Microsoft.AspNetCore.Mvc;
using NetCoreLibrary.Core.Domain;
using NetCoreLibrary.Data;

namespace NetCoreLibrary.Web.Controllers
{
    /// <summary>
    /// Fluent Validation Örnek Controller
    /// </summary>
    public class CustomersController : Controller
    {
        private readonly AppDbContext _appDbContext;
        public CustomersController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
  
        [ValidateAntiForgeryToken]
        public IActionResult Create(Customer customer)
        {
            if (!ModelState.IsValid)
                return View(customer);

            return View();
        }
    }
}
