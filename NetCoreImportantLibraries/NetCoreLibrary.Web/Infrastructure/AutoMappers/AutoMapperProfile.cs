using AutoMapper;
using NetCoreLibrary.Core.Domain;
using NetCoreLibrary.Core.DTOs;
using System;

namespace NetCoreLibrary.Web.Infrastructure.AutoMappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            VehicleMap();
            CustomerMap();
            EventMap();
        }

        public void CustomerMap()
        {
            /// <summary>
            /// Dönüştürmek istediğimiz class ile entity class ı arasında property farklılıkları var ise ForMember ile belirtmeliyiz.
            /// dest : Dönüştürmek istediğim class'ım
            /// src : dönüşüm için ihtiyacım olan class
            /// </summary>
            CreateMap<Customer, CustomerDTO>()
                .IncludeMembers(p => p.Vehicle) //Vehicle içerisindeki property'leri DTO classındaki property'lere mapleyecek (isimler aynı olmalı)
                .ForMember(dest => dest.DateOfBirth, src => src.MapFrom(p => p.BirthDay)) //Source ile Destination property isimleri farklı olduğu için automapper'a bildiriyorum.
                .ForMember(dest => dest.Mail, src => src.MapFrom(p => p.Email))
                .ForMember(dest => dest.FullName, src => src.MapFrom(p => p.NameAndLastName())) //Metot ile property eşleştirmesi
                .ForMember(dest => dest.CVV, src => src.MapFrom(p => p.CreditCard.CardValidationValue))
                .ReverseMap();
        }

        public void VehicleMap()
        {
            CreateMap<Vehicle, CustomerDTO>().ReverseMap(); //IncludeMembers için bu map'i yapmalıyız.
        }

        public void EventMap()
        {
            //Note: Dağıtık property'lerde ReverseMap() çalışmaz.

            CreateMap<EventDateDTO, EventDate>()
                .ForMember(src => src.Date, dest => dest.MapFrom(dest => new DateTime(dest.Year, dest.Month, dest.Day)));

            CreateMap<EventDate, EventDateDTO>()
                .ForMember(src => src.Year, dest => dest.MapFrom(dest => dest.Date.Year))
                .ForMember(src => src.Month, dest => dest.MapFrom(dest => dest.Date.Month))
                .ForMember(src => src.Day, dest => dest.MapFrom(dest => dest.Date.Day));
        }
    }
}
