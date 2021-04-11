using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NetCoreLibrary.Core.Domain;
using NetCoreLibrary.Core.DTOs;
using NetCoreLibrary.Data;
using System.Collections.Generic;
using System.Linq;

namespace NetCoreLibrary.Web.Controllers.WebApi
{
    /// <summary>
    /// FluentValidation ve AutoMapper test
    /// </summary>
    public class CustomersApiController : BaseApiController
    {
        private readonly IMapper _mapper;

        private readonly AppDbContext _appDbContext;
        public CustomersApiController(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        /// <summary>
        /// Fluent validation ile ModelState.IsValid durumu için oluşturduğum action
        /// </summary>
        [HttpPost]
        public IActionResult Add(Customer customer)
        {
            return Ok();
        }

        /// <summary>
        /// AutoMapper ile mapleme örneği yapmak için oluşturduğum action
        /// </summary>
        [HttpGet]
        public IActionResult GetCustomers()
        {
            var customers = _appDbContext.Customers.ToList();
            if (!customers.Any())
                return NotFound();

            //IMapper ile dönüştürme
            var customerDTOs = _mapper.Map<List<CustomerDTO>>(customers);

            return Ok(customerDTOs);
        }
    }
}
