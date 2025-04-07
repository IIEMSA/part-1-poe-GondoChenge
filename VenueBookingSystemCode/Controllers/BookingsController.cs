using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VenueBookingSystemCode.Models;

namespace VenueBookingSystemCode.Controllers
{
    public class BookingsController : Controller
    {
        private readonly ApplicationDBContext _context;

        public BookingsController(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var bookings = await _context.Booking
                .Include(b => b.Venues) // Assuming you have a navigation property for Venues
                .Include(b => b.Events)  // Assuming you have a navigation property for Events
                .ToListAsync();
            return View(bookings);
        }

        public IActionResult Create()
        {
            // Populate dropdowns for events and venues
            ViewBag.EventList = new SelectList(_context.Event_, "event_id", "event_name"); // Adjust property names as needed
            ViewBag.VenueList = new SelectList(_context.Venue, "venue_id", "venue_name"); // Adjust property names as needed
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Bookings booking)
        {
            if (ModelState.IsValid)
            {
                _context.Add(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Repopulate dropdowns if the model state is invalid
            ViewBag.EventList = new SelectList(_context.Event_, "event_id", "event_name");
            ViewBag.VenueList = new SelectList(_context.Venue, "venue_id", "venue_name");
            return View(booking);
        }

        public async Task<IActionResult> Details(int? id)
        {
            var booking = await _context.Booking
                .Include(b => b.Venues) // Include related data if needed
                .Include(b => b.Events)  // Include related data if needed
                .FirstOrDefaultAsync(m => m.booking_id == id); // Assuming booking_id is the primary key
            if (booking == null)
            {
                return NotFound();
            }
            return View(booking);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            var booking = await _context.Booking
                .Include(b => b.Venues) // Include related data if needed
                .Include(b => b.Events)  // Include related data if needed
                .FirstOrDefaultAsync(m => m.booking_id == id);
            if (booking == null)
            {
                return NotFound();
            }
            return View(booking);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var booking = await _context.Booking.FindAsync(id);
            if (booking != null)
            {
                _context.Booking.Remove(booking);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool BookingExists(int id)
        {
            return _context.Booking.Any(b => b.booking_id == id);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }

            // Populate dropdowns for events and venues
            ViewBag.EventList = new SelectList(_context.Event_, "event_id", "event_name");
            ViewBag.VenueList = new SelectList(_context.Venue, "venue_id", "venue_name");
            return View(booking);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Bookings booking)
        {
            if (id != booking.booking_id) // Assuming booking_id is the primary key
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(booking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingExists(booking.booking_id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                
            }
            return RedirectToAction(nameof(Index));


        }
    } }