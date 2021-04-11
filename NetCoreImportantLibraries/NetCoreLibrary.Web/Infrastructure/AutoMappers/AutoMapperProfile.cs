using AutoMapper;
using NetCoreLibrary.Core.Domain;
using NetCoreLibrary.Core.DTOs;

namespace NetCoreLibrary.Web.Infrastructure.AutoMappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CustomerMap();
        }

        public void CustomerMap()
        {
            CreateMap<Customer, CustomerDTO>().ReverseMap();
        }
    }
}
