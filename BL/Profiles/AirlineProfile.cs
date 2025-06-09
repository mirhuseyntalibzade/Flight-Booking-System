using AutoMapper;
using BL.DTOs.AircraftDTOs;
using BL.DTOs.AirlineDTOs;
using CORE.Models;

namespace BL.Profiles
{
    public class AirlineProfile : Profile
    {
        public AirlineProfile()
        {
            CreateMap<Airline, GetAirlineDTO>().ReverseMap();
            CreateMap<Airline, UpdateAirlineDTO>().ReverseMap();
            CreateMap<Airline, AddAirlineDTO>().ReverseMap();
            CreateMap<Airline, GetIncludedAirlineDTO>().ReverseMap();

        }
    }
}
