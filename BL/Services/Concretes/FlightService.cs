using AutoMapper;
using BL.DTOs.FlightDTOs;
using BL.Exceptions;
using BL.Services.Abstracts;
using CORE.Models;
using DAL.Contexts;
using DAL.Repositories.Abstracts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BL.Services.Concretes
{
    public class FlightService : IFlightService
    {
        readonly IRepository<Flight> _repository;
        readonly IRepository<Aircraft> _aircraftRepository;
        readonly IMapper _mapper;
        readonly AppDbContext _context;

        public FlightService(AppDbContext context, IRepository<Aircraft> aircraftRepository, IMapper mapper, IRepository<Flight> repository)
        {
            _context = context;
            _repository = repository;
            _mapper = mapper;
            _aircraftRepository = aircraftRepository;
        }

        public async Task AddFlightAsync(AddFlightDTO flightDTO)
        {
            Flight flight = _mapper.Map<Flight>(flightDTO);
            Aircraft aircraft = await _aircraftRepository.GetByIdAsync(flight.AircraftId, "SeatClasses");
            if (aircraft is null)
            {
                throw new BaseException("No aircraft found.");
            }
            if (aircraft.SeatClasses is null)
            {
                throw new BaseException("Please configure seat allocation in your aircraft first.");
            }
            foreach (var seatClass in aircraft.SeatClasses)
            {
                foreach (var column in seatClass.Columns)
                {
                    for (int row = seatClass.StartingRow; row <= seatClass.EndingRow; row++)
                    {
                        flight.Seats.Add(new Seat
                        {
                            SeatNumber = $"{column}{row}",
                            SeatClass = seatClass.ClassName,
                            IsAvailable = true,
                            AutoAssign = seatClass.AutoAssign,
                            Row = row,
                            Column = column,
                            CreatedBy = "",
                            CreatedDate = DateTime.Now
                        });
                    }
                }
            }
            await _repository.AddAsync(flight);
        }

        public async Task<ICollection<GetFlightDTO>> GetAllFlightsAsync()
        {
            return _mapper.Map<ICollection<GetFlightDTO>>(await _repository.GetAllAsync());
        }

        public async Task<GetFlightDTO> GetFlightByConditionAsync(Expression<Func<Flight, bool>> expression)
        {
            Flight flight = await _repository.GetByConditionAsync(expression, "Airline", "Aircraft", "Seats");
            if (flight is null)
            {
                throw new BaseException("Couldn't find item.");
            }
            return _mapper.Map<GetFlightDTO>(flight);
        }

        public async Task<GetFlightDTO> GetFlightByIdAsync(int id)
        {
            Flight flight = await _repository.GetByIdAsync(id, "Airline", "Aircraft", "Seats");
            if (flight is null)
            {
                throw new BaseException("Couldn't find item.");
            }
            return _mapper.Map<GetFlightDTO>(flight);
        }

        public async Task<ICollection<GetFlightDTO>> GetFlightOneWayAsync(string origin, string destination, DateTime outBound)
        {
            ICollection<GetFlightDTO> flights = await _context.Flights
                .Where(f => f.Origin.ToUpper() == origin.ToUpper())
                .Where(f => f.Destination.ToUpper() == destination.ToUpper())
                .Where(f => f.DepartureTime.Date == outBound.Date)
                .Select(f => new GetFlightDTO
                {
                    Id = f.Id,
                    Origin = f.Origin,
                    Destination = f.Destination,
                    DepartureTime = f.DepartureTime,
                    ArrivalTime = f.ArrivalTime,
                    Price = f.Price,
                    FlightNumber = f.FlightNumber,
                })
                .ToListAsync();

            return flights;
        }

        public async Task<ICollection<GetFlightDTO>> GetFlightsByRoundTripAsync(string origin, string destination, DateTime outBound, DateTime returnTime)
        {
            var outboundFlights = await _context.Flights
             .Where(f => f.Origin.ToUpper() == origin.ToUpper())
             .Where(f => f.Destination.ToUpper() == destination.ToUpper())
             .Where(f => f.DepartureTime.Date == outBound.Date)
             .Select(f => new GetFlightDTO
             {
                 Id = f.Id,
                 Origin = f.Origin,
                 Destination = f.Destination,
                 DepartureTime = f.DepartureTime,
                 ArrivalTime = f.ArrivalTime,
                 Price = f.Price
             })
             .ToListAsync();

            var returnFlights = await _context.Flights
             .Where(f => f.Origin.ToUpper() == destination.ToUpper())
             .Where(f => f.Destination.ToUpper() == origin.ToUpper())
             .Where(f => f.DepartureTime.Date == returnTime.Date)
             .Select(f => new GetFlightDTO
             {
                 Id = f.Id,
                 Origin = f.Origin,
                 Destination = f.Destination,
                 DepartureTime = f.DepartureTime,
                 ArrivalTime = f.ArrivalTime,
                 Price = f.Price
             })
             .ToListAsync();

            return outboundFlights.Concat(returnFlights).ToList();
        }

        public async Task RemoveFlightAsync(int id)
        {
            Flight flight = await _repository.GetByIdAsync(id);
            if (flight is null)
            {
                throw new BaseException("Couldn't find item.");
            }
            _repository.Remove(flight);
        }

        public async Task RevertSoftDeleteFlight(int id)
        {
            Flight flight = await _repository.GetByIdAsync(id);
            if (flight is null)
            {
                throw new BaseException("Couldn't find item.");
            }
            if (!flight.isDeleted)
            {
                throw new BaseException("Item is already active.");
            }
            _repository.RevertSoftDelete(flight);
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

        public async Task SoftDeleteFlight(int id)
        {
            Flight flight = await _repository.GetByIdAsync(id);
            if (flight is null)
            {
                throw new BaseException("Couldn't find item.");
            }
            if (flight.isDeleted)
            {
                throw new BaseException("Item is already deleted.");
            }
            _repository.SoftDelete(flight);
        }

        public async Task UpdateFlightAsync(int id, UpdateFlightDTO flightDTO)
        {
            Flight oldFlight = await _repository.GetByIdAsync(id);
            if (oldFlight is null)
            {
                throw new BaseException("Couldn't find item.");
            }
            if (oldFlight.isDeleted)
            {
                throw new BaseException("You cannot update deleted item.");
            }
            Flight flight = _mapper.Map<Flight>(flightDTO);
            flight.Id = id;
            flight.CreatedDate = oldFlight.CreatedDate;
            flight.CreatedBy = oldFlight.CreatedBy;
            _repository.Update(flight);
        }
    }
}
