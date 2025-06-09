using CORE.Models;
using DAL.Repositories.Abstracts;
using DAL.Repositories.Concretes;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public static class ConfigurationService
    {
        public static void ConfigureDAL(this IServiceCollection service)
        {
            service.AddScoped<IRepository<Aircraft>, Repository<Aircraft>>();
            service.AddScoped<IRepository<Airline>, Repository<Airline>>();
            service.AddScoped<IRepository<Booking>, Repository<Booking>>();
            service.AddScoped<IRepository<Country>, Repository<Country>>();
            service.AddScoped<IRepository<Flight>, Repository<Flight>>();
            service.AddScoped<IRepository<Passenger>, Repository<Passenger>>();
            service.AddScoped<IRepository<Seat>, Repository<Seat>>();
            service.AddScoped<IRepository<SeatClass>, Repository<SeatClass>>();
            service.AddScoped<IRepository<Blog>, Repository<Blog>>();

            service.AddHttpContextAccessor();
        }
    }
}
