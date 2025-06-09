using BL.DTOs.BookingDTOs;
using BL.DTOs.FlightDTOs;
using BL.DTOs.PassengerDTOs;
using CORE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.Abstracts
{
    public interface IBookingService
    {
        Task<ICollection<GetBookingDTO>> GetAllBookingsAsync();
        Task<GetBookingDTO> GetBookingByIdAsync(int id);
        public Task<int> CreateBooking(int outboundFlightId, int? returnFlightId, ICollection<AddPassengerDTO> passengers);
        public Task RemoveBookingAsync(int bookingId);
        public Task<string> ProcessPayment(int bookingId);
        public Task GenerateETicket(string email, int bookingId);
        public Task<int> SaveChangesAsync();

    }
}
