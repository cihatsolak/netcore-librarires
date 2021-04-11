using FluentValidation;
using FluentValidation.Results;
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

        //Custom olarak validate() metotunu kullanmak istediğimiz DI olarak ekliyoruz.
        private readonly IValidator<Customer> _customerValidator;

        public CustomersController(AppDbContext appDbContext, IValidator<Customer> customerValidator)
        {
            _appDbContext = appDbContext;
            _customerValidator = customerValidator;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Customer customer)
        {
            //ModelState ile doğrulama
            if (!ModelState.IsValid)
                return View(customer);

            //ModelState i kullanmayıp, Validate() metotu ile doğrulama yapmak.
            ValidationResult validationResult = _customerValidator.Validate(customer);
            if (!validationResult.IsValid)
                return View(customer);

            return View();
        }
    }
}
