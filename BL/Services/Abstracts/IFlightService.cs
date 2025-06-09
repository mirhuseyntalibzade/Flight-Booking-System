using BL.DTOs.FlightDTOs;
using CORE.Models;
using System.Linq.Expressions;

namespace BL.Services.Abstracts
{
    public interface IFlightService
    {
        Task<ICollection<GetFlightDTO>> GetAllFlightsAsync();
        Task<GetFlightDTO> GetFlightByIdAsync(int id);
        Task<ICollection<GetFlightDTO>> GetFlightsByRoundTripAsync(string origin, string destination, DateTime outBound, DateTime returnTime);
        Task<ICollection<GetFlightDTO>> GetFlightOneWayAsync(string origin, string destination, DateTime outBound);
        Task<GetFlightDTO> GetFlightByConditionAsync(Expression<Func<Flight, bool>> expression);
        Task AddFlightAsync(AddFlightDTO flight);
        Task UpdateFlightAsync(int id, UpdateFlightDTO flight);
        Task RemoveFlightAsync(int id);
        Task SoftDeleteFlight(int id);
        Task RevertSoftDeleteFlight(int id);
        Task<int> SaveChangesAsync();
    }
}
