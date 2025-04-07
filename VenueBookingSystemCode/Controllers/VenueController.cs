using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VenueBookingSystemCode.Models;

namespace VenueBookingSystemCode.Controllers
{
    public class VenueController : Controller
    {
        private readonly ApplicationDBContext _context;

        public VenueController(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var venue = await _context.Venue.ToListAsync();
            return View(venue);

        }

        public IActionResult Create()
        {
            return View();

        }

        [HttpPost]

    public async Task<IActionResult> Create(Venues venue)
        {

            if (ModelState.IsValid)

            {

                _context.Add(venue);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }


            return View(venue);

        }

        

    }
}
