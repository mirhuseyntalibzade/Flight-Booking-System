using BL.DTOs.AircraftDTOs;
using BL.DTOs.SeatClassDTOs;
using BL.DTOs.WrapperDTOs;
using CORE.Models;
using System.Linq.Expressions;

namespace BL.Services.Abstracts
{
    public interface IAircraftService
    {
        Task<ICollection<GetAircraftDTO>> GetAllAircraftsAsync();
        Task<GetAircraftDTO> GetAircraftByIdAsync(int id);
        Task<GetAircraftDTO> GetAircraftByConditionAsync(Expression<Func<Aircraft, bool>> expression);
        Task AddAircraftAsync(AircraftSeatClassDTO aircraftSeatClassDTO);
        Task UpdateAircraftAsync(int id, UpdateAircraftDTO airline);
        Task RemoveAircraftAsync(int id);
        Task SoftDeleteAircraft(int id);
        Task RevertSoftDeleteAircraft(int id);
        Task<int> SaveChangesAsync();
    }
}
