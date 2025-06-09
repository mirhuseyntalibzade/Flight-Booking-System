using AutoMapper;
using BL.DTOs.SeatDTOs;
using CORE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Profiles
{
    public class SeatProfile : Profile
    {
        public SeatProfile()
        {
            CreateMap<Seat, GetIncludedSeatDTO>().ReverseMap();
        }
    }
}
