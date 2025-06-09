using BL.DTOs.CountryDTOs;
using CORE.Models;
using System.Linq.Expressions;

namespace BL.Services.Abstracts
{
    public interface ICountryService
    {
        Task<ICollection<GetCountryDTO>> GetAllCountriesAsync();
        Task<GetCountryDTO> GetCountryByIdAsync(int id);
        Task<GetCountryDTO> GetCountryByConditionAsync(Expression<Func<Country,bool>> expression);
        Task AddCountryAsync(AddCountryDTO country);
        Task UpdateCountryAsync(int id, UpdateCountryDTO country);
        Task RemoveCountryAsync(int id);
        Task SoftDeleteCountry(int id);
        Task RevertSoftDeleteCountry(int id);
        Task<int> SaveChangesAsync();
    }
}
