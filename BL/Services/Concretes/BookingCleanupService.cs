using DAL.Contexts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.Concretes
{
    public class BookingCleanupService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly TimeSpan _cleanupInterval = TimeSpan.FromMinutes(15);

        public BookingCleanupService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                    var expirationTime = DateTime.Now.AddMinutes(-15); // 15 min expiration

                    var expiredBookings = dbContext.Bookings
                        .Where(b => b.Status == CORE.Enums.Status.Pending && b.CreatedDate < expirationTime)
                        .ToList();

                    if (expiredBookings.Any())
                    {
                        dbContext.Bookings.RemoveRange(expiredBookings);
                        await dbContext.SaveChangesAsync();
                    }
                }

                await Task.Delay(_cleanupInterval, stoppingToken); // Run every 15 minutes
            }
        }
    }
}
