using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VenueBookingSystemCode.Models
{
    public class Bookings
    {
        [Key] // This attribute specifies that booking_id is the primary key
        public int booking_id { get; set; }


        [ForeignKey("Events")]
        public int event_id { get; set; }
        public Events? Events { get; set; }
       
        [ForeignKey("Venues")]
        public int venue_id { get; set; }
        public Venues? Venues { get; set; }

        public DateTime booking_date { get; set; }

        
    }
}
