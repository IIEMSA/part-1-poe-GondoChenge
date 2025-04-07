using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace VenueBookingSystemCode.Models
{
    public class Events
    {
        [Key] // This attribute specifies that booking_id is the primary key
        public int event_id { get; set; }
        public string event_name { get; set; }
        public DateTime event_date { get; set; }
        public string description_ { get; set; }

        [ForeignKey("Venues")]
        public int venue_id { get; set; }
        public Venues? Venues { get; set; }

        public List<Bookings> Booking { get; set; } = new();
        
    }
}
