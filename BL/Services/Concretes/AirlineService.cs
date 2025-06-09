using AutoMapper;
using BL.AdditionalServices;
using BL.DTOs.AirlineDTOs;
using BL.Exceptions;
using BL.Services.Abstracts;
using CORE.Models;
using DAL.Repositories.Abstracts;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.Concretes
{
    public class AirlineService : IAirlineService
    {
        readonly IRepository<Airline> _repository;
        readonly IMapper _mapper;
        readonly IWebHostEnvironment _webHostEnvironment;

        public AirlineService(IWebHostEnvironment webHostEnvironment, IMapper mapper, IRepository<Airline> repository)
        {
            _repository = repository;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task AddAirlineAsync(AddAirlineDTO airlineDTO)
        {
            Airline airline = _mapper.Map<Airline>(airlineDTO);

            string folder = _webHostEnvironment.WebRootPath + "/uploads/";
            airline.LogoUrl = await ImageUpload.SaveFileAsync(airlineDTO.Logo,folder);
            
            await _repository.AddAsync(airline);
        }

        public async Task<ICollection<GetAirlineDTO>> GetAllAirlinesAsync()
        {
            return _mapper.Map<ICollection<GetAirlineDTO>>(await _repository.GetAllAsync());
        }

        public async Task<GetAirlineDTO> GetAirlineByConditionAsync(Expression<Func<Airline, bool>> expression)
        {
            Airline airline = await _repository.GetByConditionAsync(expression, "Country", "Flights", "Aircrafts");
            if (airline is null)
            {
                throw new BaseException("Couldn't find item.");
            }
            return _mapper.Map<GetAirlineDTO>(airline);
        }

        public async Task<GetAirlineDTO> GetAirlineByIdAsync(int id)
        {
            Airline airline = await _repository.GetByIdAsync(id, "Country", "Flights", "Aircrafts");
            if (airline is null)
            {
                throw new BaseException("Couldn't find item.");
            }
            return _mapper.Map<GetAirlineDTO>(airline);
        }

        public async Task RemoveAirlineAsync(int id)
        {
            Airline airline = await _repository.GetByIdAsync(id);
            if (airline is null)
            {
                throw new BaseException("Couldn't find item.");
            }
            _repository.Remove(airline);
        }

        public async Task RevertSoftDeleteAirline(int id)
        {
            Airline airline = await _repository.GetByIdAsync(id);
            if (airline is null)
            {
                throw new BaseException("Couldn't find item.");
            }
            if (!airline.isDeleted)
            {
                throw new BaseException("Item is already active.");
            }
            _repository.RevertSoftDelete(airline);
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

        public async Task SoftDeleteAirline(int id)
        {
            Airline airline = await _repository.GetByIdAsync(id);
            if (airline is null)
            {
                throw new BaseException("Couldn't find item.");
            }
            if (airline.isDeleted)
            {
                throw new BaseException("Item is already deleted.");
            }
            _repository.SoftDelete(airline);
        }

        public async Task UpdateAirlineAsync(int id, UpdateAirlineDTO airlineDTO)
        {
            Airline oldAirline = await _repository.GetByIdAsync(id);
            if (oldAirline is null)
            {
                throw new BaseException("Couldn't find item.");
            }
            if (oldAirline.isDeleted)
            {
                throw new BaseException("You cannot update deleted item.");
            }
            Airline airline = _mapper.Map<Airline>(airlineDTO);
            airline.Id = id;
            airline.CreatedDate = oldAirline.CreatedDate;
            airline.CreatedBy = oldAirline.CreatedBy;
            if (airlineDTO.Logo is null)
            {
                airline.LogoUrl = oldAirline.LogoUrl;
            }
            else
            {
                string folder = _webHostEnvironment.WebRootPath + "/uploads/";
                airline.LogoUrl = await ImageUpload.SaveFileAsync(airlineDTO.Logo, folder);
            }
            _repository.Update(airline);
        }
    }
}
