using Microsoft.EntityFrameworkCore;

namespace VenueBookingSystemCode.Models
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) 
        {
            
        }
       
      
        
       public DbSet<Venues> Venue  { get; set; }
       public DbSet<Events> Event_ { get; set; }
       public DbSet<Bookings> Booking { get; set; }



    }
 }
 



