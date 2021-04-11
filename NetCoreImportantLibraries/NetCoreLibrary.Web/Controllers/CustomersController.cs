using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using NetCoreLibrary.Core.Domain;
using System.Linq;

namespace NetCoreLibrary.Web.Controllers
{
    /// <summary>
    /// Fluent Validation Örnek Controller
    /// </summary>
    public class CustomersController : Controller
    {
        //Custom olarak validate() metotunu kullanmak istediğimiz DI olarak ekliyoruz.
        private readonly IValidator<Customer> _customerValidator;
        private readonly IValidator<Address> _addressValidator;

        public CustomersController(IValidator<Customer> customerValidator, IValidator<Address> addressValidator)
        {
            _customerValidator = customerValidator;
            _addressValidator = addressValidator;
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

            #region ModelState'i kullanmasaydım?
            //ModelState i kullanmayıp, Validate() metotu ile customerın doğrulamasını yapmak.
            ValidationResult validationResult = _customerValidator.Validate(customer);
            if (!validationResult.IsValid)
                return View(customer);

            //ModelState i kullanmayıp, Validate() metotu ile adreslerin doğrulamasını yapmak.
            bool isValid = true;
            customer.Addresses.ToList().ForEach(address =>
            {
                ValidationResult validationResult = _addressValidator.Validate(address);
                if (!validationResult.IsValid)
                {
                    isValid = false;
                    return;
                }
            });

            if (!isValid)
                return View(customer);

            #endregion

            return View();
        }
    }
}
