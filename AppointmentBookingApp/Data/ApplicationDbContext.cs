using AppointmentBookingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace AppointmentBookingApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Appointment> Appointments { get; set; }
    }
}