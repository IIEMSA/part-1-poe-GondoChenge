using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Azure.Storage.Blobs;
using VenueBookingSystemCode.Models;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Linq;

namespace VenueBookingSystemCode.Controllers
{
    public class VenueController : Controller
    {
        private readonly ApplicationDBContext _context;

        public VenueController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: Venue
        public async Task<IActionResult> Index()
        {
            var venues = await _context.Venue.ToListAsync();
            return View(venues);
        }

        // GET: Venue/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Venue/Create
        [HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Create(Venues venue)
{
    if (ModelState.IsValid)
    {
        if (venue.ImageFile != null)
        {
            var blobUrl = await UploadImageToBlobAsync(venue.ImageFile);
            venue.image_url = blobUrl;
        }

        _context.Add(venue);
        await _context.SaveChangesAsync();
        TempData["SuccessMessage"] = "Venue created successfully.";
        return RedirectToAction(nameof(Index));
    }
    else
    {
        // Log validation errors
        var errors = ModelState.Values.SelectMany(v => v.Errors);
        foreach (var error in errors)
        {
            // Log or inspect the error
            Console.WriteLine(error.ErrorMessage);
        }
    }
    return View(venue);
}


        // GET: Venue/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var venue = await _context.Venue.FirstOrDefaultAsync(m => m.venue_id == id);
            if (venue == null)
            {
                return NotFound();
            }
            return View(venue);
        }

        // GET: Venue/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var venue = await _context.Venue.FirstOrDefaultAsync(m => m.venue_id == id);
            if (venue == null)
            {
                return NotFound();
            }
            return View(venue);
        }

        // POST: Venue/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var venue = await _context.Venue.FindAsync(id);
            if (venue != null)
            {
                _context.Venue.Remove(venue);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Venue deleted successfully.";
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Venue/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venue = await _context.Venue.FindAsync(id);
            if (venue == null)
            {
                return NotFound();
            }

            return View(venue);
        }

        // POST: Venue/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Venues venue)
        {
            if (id != venue.venue_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (venue.ImageFile != null)
                    {

                        //Upload a new image only if it is provided
                        var blobUrl = await UploadImageToBlobAsync(venue.ImageFile);
                        
                        //step 6
                        venue.image_url = blobUrl;


                    }
                    else
                    {
                        // Maintain existing Image URL by fetching original record
                        var existingVenue = await _context.Venue
                            .AsNoTracking()
                            .FirstOrDefaultAsync(v => v.venue_id == id);
                        if (existingVenue != null)
                        {
                            venue.image_url = existingVenue.image_url;
                        }
                    }

                    _context.Update(venue);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Venue updated successfully.";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VenueExists(venue.venue_id))
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
            return View(venue);
        }

        private bool VenueExists(int id)
        {
            return _context.Venue.Any(e => e.venue_id == id);
        }

        // Uploads an image to Azure Blob Storage and returns the Blob URL
        private async Task<string> UploadImageToBlobAsync(IFormFile imageFile)
        {
            var connectionString = "DefaultEndpointsProtocol=https;AccountName=chengestorage;AccountKey=cClPKHQADMxbx64cldnAaLo1b2jLwdznR+7SKDMsQPQoIFqxw3e0d3NkXK5ibuahZGZ1UhX+j8V6+AStceJ9fA==;EndpointSuffix=core.windows.net"; // TODO: Replace with your Azure Blob Storage connection string
            var containerName = "cldv6211chenge"; // TODO: Replace with your Blob container name

            var blobServiceClient = new BlobServiceClient(connectionString);
            var containerClient = blobServiceClient.GetBlobContainerClient(containerName);

            // Ensure container exists (if you want to create if not exists)
            await containerClient.CreateIfNotExistsAsync();

            var blobClient = containerClient.GetBlobClient(Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName));

            var blobHttpHeaders = new Azure.Storage.Blobs.Models.BlobHttpHeaders
            {
                ContentType = imageFile.ContentType
            };

            using (var stream = imageFile.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, new Azure.Storage.Blobs.Models.BlobUploadOptions
                {
                    HttpHeaders = blobHttpHeaders
                });
            }

            return blobClient.Uri.ToString();
        }
    }
}








