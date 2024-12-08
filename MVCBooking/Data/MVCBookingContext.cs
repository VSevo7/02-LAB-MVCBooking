using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MVCBooking.Models;

namespace MVCBooking.Data
{
    public class MVCBookingContext : DbContext
    {
        public MVCBookingContext (DbContextOptions<MVCBookingContext> options)
            : base(options)
        {
        }

        public DbSet<MVCBooking.Models.HotelBooking> HotelBooking { get; set; } = default!;
    }
}
