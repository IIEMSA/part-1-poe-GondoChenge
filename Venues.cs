using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace VenueBookingSystemCode.Models
{
    public class Venues
    {
        [Key] // This attribute specifies that booking_id is the primary key
        public int venue_id { get; set; }
        public string venue_name { get; set; }
        public string location_of_venue { get; set; }
        public int capacity { get; set; }
        public string image_url { get; set; }

        [NotMapped]

        public IFormFile? ImageFile { get; set; }

        public List<Bookings> Booking { get; set; } = new();
    }
}
