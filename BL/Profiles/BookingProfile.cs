using AutoMapper;
using BL.DTOs.BookingDTOs;
using CORE.Models;

namespace BL.Profiles
{
    public class BookingProfile : Profile
    {
        public BookingProfile()
        {
            CreateMap<Booking, GetBookingDTO>().ReverseMap();
        }
    }
}
