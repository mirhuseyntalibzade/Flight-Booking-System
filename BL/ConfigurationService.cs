using BL.Services.Abstracts;
using BL.Services.Concretes;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BL
{
    public static class ConfigurationService
    {
        public static void ConfigureBL(this IServiceCollection service)
        {
            service.AddAutoMapper(Assembly.GetExecutingAssembly());

            service.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            service.AddFluentValidationAutoValidation();
            service.AddFluentValidationClientsideAdapters();

            service.AddScoped<IAuthService, AuthService>();
            service.AddScoped<ICountryService, CountryService>();
            service.AddScoped<IAirlineService, AirlineService>();
            service.AddScoped<IAircraftService, AircraftService>();
            service.AddScoped<IFlightService, FlightService>();
            service.AddScoped<IBookingService, BookingService>();
            service.AddScoped<IEmailService, EmailService>();
            service.AddScoped<IPaymentService, PaymentService>();
            service.AddScoped<IBlogService, BlogService>();
        }
    }
}
