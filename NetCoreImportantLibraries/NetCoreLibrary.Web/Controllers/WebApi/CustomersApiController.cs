using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NetCoreLibrary.Core.Domain;
using NetCoreLibrary.Core.DTOs;
using NetCoreLibrary.Data;
using NetCoreLibrary.Web.Infrastructure.AutoMappers;
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

        /// <summary>
        /// AutoMapper ile ComplexType mapping işlemi.
        /// Customer içerisindeki CreditCard class'ını CustomerDTO property'lerine map ediyoruz. (İsimlendirme kuralı)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult FlatteningAndIncludeMembers()
        {
            var customer = GenerateFakeCustomer();

            var customerDTO = _mapper.Map<CustomerDTO>(customer);
            var customerDTO2 = ObjectMapper.Mapper.Map<CustomerDTO>(customer); //Alternatif, Lazy

            return Ok(customerDTO);
        }

        /// <summary>
        /// Bir property'i birden fazla property'e dağıtıyoruz, ya da dağıtık property'i tek bir property'de birleştiriyoruz.
        /// </summary>

        [HttpPost]
        public IActionResult Projection([FromBody]EventDateDTO eventDateDTO)
        {
            EventDate eventDate = ObjectMapper.Mapper.Map<EventDate>(eventDateDTO);

            return Ok();
        }
    }
}
