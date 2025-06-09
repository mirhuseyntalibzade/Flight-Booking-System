using AutoMapper;
using BL.DTOs.AircraftDTOs;
using BL.DTOs.SeatClassDTOs;
using BL.DTOs.WrapperDTOs;
using BL.Exceptions;
using BL.Services.Abstracts;
using CORE.Models;
using DAL.Repositories.Abstracts;
using System.Linq.Expressions;

namespace BL.Services.Concretes
{
    public class AircraftService : IAircraftService
    {
        readonly IRepository<Aircraft> _repository;
        readonly IMapper _mapper;

        public AircraftService(IMapper mapper, IRepository<Aircraft> repository)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task AddAircraftAsync(AircraftSeatClassDTO aircraftSeatClassDTO)
        {
            Aircraft aircraft = _mapper.Map<Aircraft>(aircraftSeatClassDTO.Aircraft);

            foreach (var item in aircraftSeatClassDTO.SeatClasses)
            {
                SeatClass seatClass = new SeatClass
                {
                    ClassName = item.ClassName,
                    StartingRow = item.StartingRow,
                    EndingRow = item.EndingRow,
                    Columns = item.Columns,
                    AutoAssign = item.AutoAssign,
                    CreatedBy = "",
                    CreatedDate = DateTime.Now
                };
                aircraft.SeatClasses.Add(seatClass);
            }

            await _repository.AddAsync(aircraft);
        }

        public async Task<ICollection<GetAircraftDTO>> GetAllAircraftsAsync()
        {
            return _mapper.Map<ICollection<GetAircraftDTO>>(await _repository.GetAllAsync());
        }

        public async Task<GetAircraftDTO> GetAircraftByConditionAsync(Expression<Func<Aircraft, bool>> expression)
        {
            Aircraft aircraft = await _repository.GetByConditionAsync(expression, "Airline", "SeatClasses", "Flights");
            if (aircraft is null)
            {
                throw new BaseException("Couldn't find item.");
            }
            return _mapper.Map<GetAircraftDTO>(aircraft);
        }

        public async Task<GetAircraftDTO> GetAircraftByIdAsync(int id)
        {
            Aircraft aircraft = await _repository.GetByIdAsync(id, "Airline", "SeatClasses", "Flights");
            if (aircraft is null)
            {
                throw new BaseException("Couldn't find item.");
            }
            return _mapper.Map<GetAircraftDTO>(aircraft);
        }

        public async Task RemoveAircraftAsync(int id)
        {
            Aircraft aircraft = await _repository.GetByIdAsync(id);
            if (aircraft is null)
            {
                throw new BaseException("Couldn't find item.");
            }
            _repository.Remove(aircraft);
        }

        public async Task RevertSoftDeleteAircraft(int id)
        {
            Aircraft aircraft = await _repository.GetByIdAsync(id);
            if (aircraft is null)
            {
                throw new BaseException("Couldn't find item.");
            }
            if (!aircraft.isDeleted)
            {
                throw new BaseException("Item is already active.");
            }
            _repository.RevertSoftDelete(aircraft);
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

        public async Task SoftDeleteAircraft(int id)
        {
            Aircraft aircraft = await _repository.GetByIdAsync(id);
            if (aircraft is null)
            {
                throw new BaseException("Couldn't find item.");
            }
            if (aircraft.isDeleted)
            {
                throw new BaseException("Item is already deleted.");
            }
            _repository.SoftDelete(aircraft);
        }

        public async Task UpdateAircraftAsync(int id, UpdateAircraftDTO aircraftDTO)
        {
            Aircraft oldAircraft = await _repository.GetByIdAsync(id);
            if (oldAircraft is null)
            {
                throw new BaseException("Couldn't find item.");
            }
            if (oldAircraft.isDeleted)
            {
                throw new BaseException("You cannot update deleted item.");
            }
            Aircraft aircraft = _mapper.Map<Aircraft>(aircraftDTO);
            aircraft.Id = id;
            aircraft.CreatedDate = oldAircraft.CreatedDate;
            aircraft.CreatedBy = oldAircraft.CreatedBy;
            _repository.Update(aircraft);
        }
    }
}
