using AutoMapper;
using BL.DTOs.FlightDTOs;
using CORE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Profiles
{
    public class FlightProfile : Profile
    {
        public FlightProfile()
        {
            CreateMap<Flight, GetFlightDTO>().ReverseMap();
            CreateMap<Flight, UpdateFlightDTO>().ReverseMap();
            CreateMap<Flight, AddFlightDTO>().ReverseMap();
            CreateMap<Flight, GetIncludedFlightDTO>().ReverseMap();
        }
    }
}
