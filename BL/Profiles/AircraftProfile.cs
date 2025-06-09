using AutoMapper;
using BL.DTOs.AircraftDTOs;
using CORE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Profiles
{
    public class AircraftProfile : Profile
    {
        public AircraftProfile()
        {
            CreateMap<Aircraft, GetAircraftDTO>().ReverseMap();
            CreateMap<Aircraft, UpdateAircraftDTO>().ReverseMap();
            CreateMap<Aircraft, AddAircraftDTO>().ReverseMap();
            CreateMap<Aircraft, GetIncludedAircraftDTO>().ReverseMap();
        }
    }
}