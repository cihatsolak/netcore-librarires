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
            /// <summary>
            /// Dönüştürmek istediğimiz class ile entity class ı arasında property farklılıkları var ise ForMember ile belirtmeliyiz.
            /// dest : Dönüştürmek istediğim class'ım
            /// src : dönüşüm için ihtiyacım olan class
            /// </summary>
            CreateMap<Customer, CustomerDTO>()
                .ForMember(dest => dest.DateOfBirth, src => src.MapFrom(p => p.BirthDay)) //Source ile Destination property isimleri farklı olduğu için automapper'a bildiriyorum.
                .ForMember(dest => dest.Mail, src => src.MapFrom(p => p.Email))
                .ReverseMap();
        }
    }
}
