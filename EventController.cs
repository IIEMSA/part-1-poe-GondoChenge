using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VenueBookingSystemCode.Models;
using System.Threading.Tasks;

namespace VenueBookingSystemCode.Controllers
{
    public class EventController : Controller
    {
        private readonly ApplicationDBContext _context;

        public EventController(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var eEvent = await _context.Event_.ToListAsync(); // Using Event_ DbSet
            return View(eEvent);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Events Event_)
        {
            if (ModelState.IsValid)
            {
                _context.Add(Event_);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(Event_);
        }

        public async Task<IActionResult> Details(int? id)
        {
            var eEvent = await _context.Event_.FirstOrDefaultAsync(m => m.event_id == id);
            if (eEvent == null)
            {
                return NotFound();
            }
            return View(eEvent);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            var eEvent = await _context.Event_.FirstOrDefaultAsync(m => m.event_id == id);
            if (eEvent == null)
            {
                return NotFound();
            }
            return View(eEvent);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var eEvent = await _context.Event_.FindAsync(id);
            if (eEvent != null)
            {
                _context.Event_.Remove(eEvent);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool EventExists(int id)
        {
            return _context.Event_.Any(e => e.event_id == id);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eEvent = await _context.Event_.FindAsync(id);
            if (eEvent == null)
            {
                return NotFound();
            }

            return View(eEvent);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Events eEvent)
        {
            if (id != eEvent.event_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eEvent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(eEvent.event_id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(eEvent);
        }
    }
}

