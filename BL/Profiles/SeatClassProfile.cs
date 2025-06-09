using AutoMapper;
using BL.DTOs.SeatClassDTOs;
using CORE.Models;

namespace BL.Profiles
{
    public class SeatClassProfile : Profile
    {
        public SeatClassProfile()
        {
            CreateMap<SeatClass, GetSeatClassDTO>().ReverseMap();
            CreateMap<SeatClass, UpdateSeatClassDTO>().ReverseMap();
            CreateMap<SeatClass, AddSeatClassDTO>().ReverseMap();
            CreateMap<SeatClass, GetIncludedSeatClassDTO>().ReverseMap();
        }
    }
}
