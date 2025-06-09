using AutoMapper;
using BL.DTOs.AircraftDTOs;
using BL.DTOs.AirlineDTOs;
using BL.DTOs.BookingDTOs;
using BL.DTOs.FlightDTOs;
using BL.DTOs.PassengerDTOs;
using BL.Exceptions;
using BL.Services.Abstracts;
using CORE.Models;
using DAL.Contexts;
using DAL.Repositories.Abstracts;
using Microsoft.EntityFrameworkCore;
using Stripe.Checkout;
using System.Linq.Expressions;

namespace BL.Services.Concretes
{
    public class BookingService : IBookingService
    {
        readonly IRepository<Flight> _repository;
        readonly IRepository<Booking> _bookingRepository;
        readonly IRepository<Seat> _seatRepository;
        readonly IEmailService _emailService;
        readonly AppDbContext _context;
        readonly IMapper _mapper;

        public BookingService(IMapper mapper, AppDbContext context, IEmailService emailService, IRepository<Seat> seatRepository, IRepository<Booking> bookingRepository, IRepository<Flight> repository)
        {
            _mapper = mapper;
            _context = context;
            _repository = repository;
            _bookingRepository = bookingRepository;
            _seatRepository = seatRepository;
            _emailService = emailService;
        }
        public async Task<int> CreateBooking(int outboundFlightId, int? returnFlightId, ICollection<AddPassengerDTO> passengers)
        {
            Flight outboundFlight = await _repository.GetByIdAsync(outboundFlightId, "Seats");
            if (outboundFlight == null) throw new BaseException("Outbound flight not found.");

            Flight returnFlight = null;
            if (returnFlightId.HasValue)
            {
                returnFlight = await _repository.GetByIdAsync(returnFlightId.Value, "Seats");
                if (returnFlight == null) throw new BaseException("Return flight not found.");
            }

            string pnr = GeneratePNR();
            Booking booking = new Booking
            {
                PNR = pnr,
                Status = CORE.Enums.Status.Pending,
                Passengers = new List<Passenger>(),
                NumberOfPassengers = passengers.Count,
                TotalPrice = passengers.Count * (outboundFlight.Price + (returnFlight?.Price ?? 0)),
                BookingFlights = new List<BookingFlight>
                {
                    new BookingFlight { FlightId = outboundFlightId }
                }
            };

            if (returnFlight != null)
            {
                booking.BookingFlights.Add(new BookingFlight { FlightId = returnFlightId.Value });
            }

            List<Seat> outboundSeats = AssignSeats(outboundFlight, passengers.Count);
            if (outboundSeats.Count < passengers.Count) throw new BaseException("Not enough available seats on outbound flight.");

            List<Seat> returnSeats = new List<Seat>();
            if (returnFlight != null)
            {
                returnSeats = AssignSeats(returnFlight, passengers.Count);
                if (returnSeats.Count < passengers.Count) throw new BaseException("Not enough available seats on return flight.");
            }

            int seatIndex = 0;
            foreach (AddPassengerDTO p in passengers)
            {
                Passenger outboundPassenger = new Passenger
                {
                    Name = p.Name,
                    Surname = p.Surname,
                    DOB = p.DOB,
                    PassportNumber = p.PassportNumber,
                    CreatedDate = DateTime.Now,
                    CreatedBy = "",
                    SeatId = outboundSeats[seatIndex].Id
                };
                booking.Passengers.Add(outboundPassenger);
                AssignSeatToPassenger(outboundSeats[seatIndex], outboundPassenger);

                if (returnFlight != null)
                {
                    Passenger returnPassenger = new Passenger
                    {
                        Name = p.Name,
                        Surname = p.Surname,
                        DOB = p.DOB,
                        PassportNumber = p.PassportNumber,
                        CreatedDate = DateTime.Now,
                        CreatedBy = "",
                        SeatId = returnSeats[seatIndex].Id
                    };
                    booking.Passengers.Add(returnPassenger);
                    AssignSeatToPassenger(returnSeats[seatIndex], returnPassenger);
                }

                seatIndex++;
            }

            await _bookingRepository.AddAsync(booking);
            await _bookingRepository.SaveChangesAsync();
            return booking.Id;
        }

        private List<Seat> AssignSeats(Flight flight, int passengerCount)
        {
            List<Seat> availableSeats = flight.Seats
                .Where(s => s.IsAvailable && s.AutoAssign)
                .OrderBy(s => s.Row)
                .ThenBy(s => s.Column)
                .ToList();

            if (availableSeats.Count < passengerCount)
                return new List<Seat>();

            List<Seat> assignedSeats = new List<Seat>();

            if (passengerCount == 1)
            {
                Random rnd = new Random();
                assignedSeats.Add(availableSeats[rnd.Next(availableSeats.Count)]);
            }
            else
            {
                var seatGroups = availableSeats.GroupBy(s => s.Row)
                    .OrderByDescending(g => g.Count())
                    .ToList();

                int remainingPassengers = passengerCount;
                foreach (var group in seatGroups)
                {
                    if (group.Count() >= remainingPassengers)
                    {
                        assignedSeats.AddRange(group.Take(remainingPassengers));
                        break;
                    }
                    else if (group.Count() > 1)
                    {
                        assignedSeats.AddRange(group);
                        remainingPassengers -= group.Count();
                    }
                }

                if (assignedSeats.Count < passengerCount)
                {
                    assignedSeats.AddRange(availableSeats.Except(assignedSeats).Take(passengerCount - assignedSeats.Count));
                }
            }

            return assignedSeats;
        }

        private void AssignSeatToPassenger(Seat seat, Passenger passenger)
        {
            seat.IsAvailable = false;
            seat.Passenger = passenger;
            _seatRepository.Update(seat);
        }

        public async Task<string> ProcessPayment(int bookingId)
        {
            Booking booking = await _bookingRepository.GetByIdAsync(bookingId);
            if (booking == null || booking.Status != CORE.Enums.Status.Pending)
                throw new BaseException("Booking not found or already confirmed.");

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>
            {
            new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
            {
                Currency = "usd",
                ProductData = new SessionLineItemPriceDataProductDataOptions
                {
                    Name = "Flight Booking"
                },
                    UnitAmount = (long)(booking.TotalPrice * 100)
                },
                    Quantity = 1
                    }
                },
                Mode = "payment",
                SuccessUrl = $"http://localhost:5173/success?session_id={{CHECKOUT_SESSION_ID}}",
                CancelUrl = "http://localhost:5173/cancel",
                Metadata = new Dictionary<string, string>
                {
                    { "booking_id", $"{bookingId}" } // Generate a real Booking ID dynamically
                }
            };

            var service = new SessionService();
            Session session = await service.CreateAsync(options);


            booking.StripeSessionId = session.Id;
            _bookingRepository.Update(booking);

            return session.Url;
        }


        public async Task RemoveBookingAsync(int bookingId)
        {
            Booking booking = await _bookingRepository.GetByIdAsync(bookingId, "Passengers");
            if (booking == null || booking.Status != CORE.Enums.Status.Pending)
                throw new BaseException("Booking not found or already confirmed.");

            foreach (var passenger in booking.Passengers)
            {
                if (passenger.SeatId.HasValue)
                {
                    var seat = await _seatRepository.GetByIdAsync(passenger.SeatId.Value);
                    if (seat != null)
                    {
                        seat.IsAvailable = true;
                        seat.PassengerId = null;
                        _seatRepository.Update(seat);
                    }
                }
            }

            _bookingRepository.Remove(booking);
        }


        public async Task GenerateETicket(string email, int bookingId)
        {

            Booking booking = await _context.Bookings
    .Include(b => b.Passengers)
        .ThenInclude(p => p.Seat)
    .Include(b => b.BookingFlights)
        .ThenInclude(bf => bf.Flight)
            .ThenInclude(f => f.Seats)
    .FirstOrDefaultAsync(b => b.Id == bookingId);

            if (booking == null) throw new BaseException("Booking not found.");

            if (booking.Passengers == null || !booking.Passengers.Any())
                throw new BaseException("No passengers found for this booking.");

            List<string> ticketDetails = new List<string>();

            foreach (Passenger passenger in booking.Passengers)
            {
                Flight flight = booking.BookingFlights
                    .FirstOrDefault(f => f.Flight.Seats.Any(s => s.Id == passenger.SeatId))?.Flight;

                if (flight == null) continue;

                string ticket = $@"
<!DOCTYPE html>
<html>
<head>
    <title>E-Ticket</title>
</head>
<body style='font-family: Arial, sans-serif; margin: 0; padding: 0; background-color: #f4f4f4;'>
    <table width='100%' cellspacing='0' cellpadding='0'>
        <tr>
            <td align='center'>
                <table width='400px' style='background: #ffffff; border-radius: 10px; box-shadow: 0px 4px 10px rgba(0, 0, 0, 0.1); padding: 20px; margin: 20px;'>
                    <!-- Header -->
                    <tr>
                        <td align='center'>
                            <h2 style='color: #333;'>✈ Boarding Pass</h2>
                            <p style='font-size: 14px; color: #666;'>PNR: <strong>{booking.PNR}</strong></p>
                        </td>
                    </tr>
                    
                    <!-- Passenger Details -->
                    <tr>
                        <td style='padding: 10px 0;'>
                            <p><strong>Passenger:</strong> {passenger.Name} {passenger.Surname}</p>
                            <p><strong>Flight:</strong> {flight.Origin} → {flight.Destination}</p>
                            <p><strong>Date:</strong> {flight.DepartureTime:yyyy-MM-dd HH:mm}</p>
                            <p><strong>Seat:</strong> {passenger.Seat?.Row}{passenger.Seat?.Column}</p>
                        </td>
                    </tr>

                    <!-- Footer -->
                    <tr>
                        <td align='center' style='padding: 10px; font-size: 12px; color: #888;'>
                            <p>Thank you for choosing VoxaFly!</p>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</body>
</html>";


                ticketDetails.Add(ticket);
            }

            string emailBody = $"Dear Passenger,\n\nYour e-tickets are attached below:\n\n{string.Join("\n\n", ticketDetails)}";

            await _emailService.SendEmailAsync(email, "Your E-Tickets", emailBody);
        }

        private string GeneratePNR()
        {
            return Guid.NewGuid().ToString("N").Substring(0, 6).ToUpper();
        }

        public async Task<int> SaveChangesAsync()
        {
            int result = await _bookingRepository.SaveChangesAsync();
            if (result == 0)
            {
                throw new BaseException("Couldn't save chagnes.");
            }
            return result;
        }

        public async Task<ICollection<GetBookingDTO>> GetAllBookingsAsync()
        {
            return _mapper.Map<ICollection<GetBookingDTO>>(await _repository.GetAllAsync());
        }
        public async Task<GetBookingDTO> GetBookingByIdAsync(int id)
        {
            Booking booking = await _bookingRepository.GetByIdAsync(id, "Passengers");
            if (booking is null)
            {
                throw new BaseException("Couldn't find item.");
            }
            return _mapper.Map<GetBookingDTO>(booking);
        }



    }
}
