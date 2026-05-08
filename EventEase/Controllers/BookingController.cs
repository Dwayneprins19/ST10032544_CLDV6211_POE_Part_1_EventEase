using EventEase.Data;
using EventEase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EventEase.Controllers
{
    public class BookingController : Controller
    {
        private readonly AppDbContext _context;

        public BookingController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string searchString)
        {
            var bookings = _context.Bookings
                .Include(b => b.Event)
                .Include(b => b.Venue)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                bookings = bookings.Where(b => 
                    b.BookingId.ToString().Contains(searchString) ||
                    b.Event!.Name.Contains(searchString));
            }
                return View(await bookings.ToListAsync());
        }

        public IActionResult Create()
        {
            ViewData["VenueId"] = new SelectList(_context.Venues, "VenueId", "Name");

            ViewData["EventId"] = new SelectList(_context.Events, "EventId", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Booking booking)
        {
            bool exists = _context.Bookings.Any(b => 
                b.VenueId == booking.VenueId && 
                b.BookingDate == booking.BookingDate && 
                booking.StartTime < b.EndTime &&
                booking.EndTime > b.StartTime
                );
            if (!exists)
            {
                ModelState.AddModelError("", "Venue has already been booked for that time.");
            }

            if (ModelState.IsValid)
            {
                _context.Add(booking);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewData["VenueId"] = new SelectList(_context.Venues, "VenueId", "Name", booking.VenueId);
            ViewData["EventId"] = new SelectList(_context.Events, "EventId", "Name", booking.EventId);

            return View(booking);
        }
    }
}
