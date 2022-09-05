
using AutoMapper;
using EFCoreCodeFirstSample.Models;
using EFCoreCodeFirstSample.Data;
using EFCoreCodeFirstSample.Core.DTOs;

namespace EFCoreCodeFirstSample.Configurations
{
    public class MapperInitilizer: Profile
    {
        public MapperInitilizer()
        {
            CreateMap<Country, CountryDTO>().ReverseMap();
            CreateMap<Country, CreateCountryDTO>().ReverseMap();
            CreateMap<Employee, LoginEmployeeDTO>().ReverseMap();
            CreateMap<Employee, UpdateEmployeeDTO>().ReverseMap();
            CreateMap<Hotel, HotelDTO>().ReverseMap();
            CreateMap<Hotel, CreateHotelDTO>().ReverseMap();
            CreateMap<ApiUser, UserDTO>().ReverseMap();

        }
    }
}
