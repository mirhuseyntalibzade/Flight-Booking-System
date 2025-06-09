using AutoMapper;
using BL.DTOs.PassengerDTOs;
using CORE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Profiles
{
    public class PassengerProfile : Profile
    {
        public PassengerProfile()
        {
            CreateMap<Passenger, AddPassengerDTO>();
            CreateMap<Passenger, UpdatePassengerDTO>();
        }
    }
}
