using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetCoreLibrary.Data;
using NetCoreLibrary.Web.Models;
using System;
using System.Diagnostics;
using System.Linq;

namespace NetCoreLibrary.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _appDbContext;
        public HomeController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IActionResult Index()
        {
            if (false) //Unreachable code detected
            {
                throw new Exception("Örnek Hata");
            }
            return View();
        }

        public IActionResult UseDeveloperExceptionPageTest()
        {
            var customer = _appDbContext.Customers.SingleOrDefault(p => p.Id == 999);
            return View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
