using Microsoft.AspNetCore.Mvc;
using NetCoreLibrary.Core.Domain;
using NetCoreLibrary.Core.Enums;
using System;
using System.Collections.Generic;

namespace NetCoreLibrary.Web.Controllers.WebApi
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Produces("application/json")] //Dönüş json tipinde.
    [Consumes("application/json")] //Request kesinlikle json tipinde olmalı.
    public class BaseApiController : ControllerBase
    {

        [NonAction]
        internal static Customer GenerateFakeCustomer()
        {
            var customer = new Customer
            {
                Id = 1,
                Name = "Cihat",
                LastName = "Solak",
                BirthDay = DateTime.Now,
                Age = 20,
                Email = "test@test.com",
                Gender = Gender.Female,
                Addresses = new List<Address>
                {
                    new Address()
                    {
                        Id = 1,
                        Province = "İstanbul",
                        Content = "Test Adres",
                        PostCode = "85A3D3",
                    }
                },
                CreditCard = new CreditCard
                {
                    Number = "2585 2222 0514 9562",
                    ValidityDate = DateTime.Now.AddYears(2)
                },
                Vehicle = new Vehicle
                {
                    Plate = "34ABC123",
                    Color = "Beyaz"
                }
            };

            return customer;
        }
    }
}
