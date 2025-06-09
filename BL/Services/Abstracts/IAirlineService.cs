using BL.DTOs.AirlineDTOs;
using CORE.Models;
using System.Linq.Expressions;

namespace BL.Services.Abstracts
{
    public interface IAirlineService
    {
        Task<ICollection<GetAirlineDTO>> GetAllAirlinesAsync();
        Task<GetAirlineDTO> GetAirlineByIdAsync(int id);
        Task<GetAirlineDTO> GetAirlineByConditionAsync(Expression<Func<Airline, bool>> expression);
        Task AddAirlineAsync(AddAirlineDTO airline);
        Task UpdateAirlineAsync(int id, UpdateAirlineDTO airline);
        Task RemoveAirlineAsync(int id);
        Task SoftDeleteAirline(int id);
        Task RevertSoftDeleteAirline(int id);
        Task<int> SaveChangesAsync();
    }
}
