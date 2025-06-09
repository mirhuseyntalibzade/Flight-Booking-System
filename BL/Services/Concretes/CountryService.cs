using AutoMapper;
using BL.DTOs.CountryDTOs;
using BL.Exceptions;
using BL.Services.Abstracts;
using CORE.Models;
using DAL.Repositories.Abstracts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
namespace BL.Services.Concretes
{
    public class CountryService : ICountryService
    {
        readonly IRepository<Country> _repository;
        readonly IMapper _mapper;

        public CountryService(IMapper mapper, IRepository<Country> repository)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task AddCountryAsync(AddCountryDTO country)
        {
            await _repository.AddAsync(_mapper.Map<Country>(country));
        }

        public async Task<ICollection<GetCountryDTO>> GetAllCountriesAsync()
        {
            return _mapper.Map<ICollection<GetCountryDTO>>(await _repository.GetAllAsync());
        }

        public async Task<GetCountryDTO> GetCountryByConditionAsync(Expression<Func<Country, bool>> expression)
        {
            Country country = await _repository.GetByConditionAsync(expression, "Airlines");
            if (country is null)
            {
                throw new BaseException("Couldn't find item.");
            }
            return _mapper.Map<GetCountryDTO>(country);
        }

        public async Task<GetCountryDTO> GetCountryByIdAsync(int id)
        {
            Country country = await _repository.GetByIdAsync(id, "Airlines");
            if (country is null)
            {
                throw new BaseException("Couldn't find item.");
            }
            return _mapper.Map<GetCountryDTO>(country);
        }
        public async Task RemoveCountryAsync(int id)
        {
            Country country = await _repository.GetByIdAsync(id);
            if (country is null)
            {
                throw new BaseException("Couldn't find item.");
            }
            _repository.Remove(country);
        }

        public async Task RevertSoftDeleteCountry(int id)
        {
            Country country = await _repository.GetByIdAsync(id);
            if (country is null)
            {
                throw new BaseException("Couldn't find item.");
            }
            if (!country.isDeleted)
            {
                throw new BaseException("Item is already active.");
            }
            _repository.RevertSoftDelete(country);
        }

        public async Task<int> SaveChangesAsync()
        {
            int result = await _repository.SaveChangesAsync();
            if (result == 0)
            {
                throw new BaseException("Couldn't save changes.");
            }
            return result;
        }

        public async Task SoftDeleteCountry(int id)
        {
            Country country = await _repository.GetByIdAsync(id);
            if (country is null)
            {
                throw new BaseException("Couldn't find item.");
            }
            if (country.isDeleted)
            {
                throw new BaseException("Item is already deleted.");
            }
            _repository.SoftDelete(country);
        }

        public async Task UpdateCountryAsync(int id, UpdateCountryDTO countryDTO)
        {
            Country oldCountry = await _repository.GetByIdAsync(id);
            if (oldCountry is null)
            {
                throw new BaseException("Couldn't find item.");
            }
            if (oldCountry.isDeleted)
            {
                throw new BaseException("You cannot update deleted item.");
            }
            Country country = _mapper.Map<Country>(countryDTO);
            country.Id = id;
            country.CreatedDate = oldCountry.CreatedDate;
            country.CreatedBy = oldCountry.CreatedBy;
            _repository.Update(country);
        }
    }
}
