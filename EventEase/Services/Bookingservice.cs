using EventEase.Data;

namespace EventEase.Services
{
    public class Bookingservice
    {
        private readonly AppDbContext _context;

        public Bookingservice(AppDbContext context)
        {
            _context = context;
        }

        public bool IsVenueAvailable(int venueId, DateTime bookingDate)
        {
            return !_context.Bookings.Any(b =>
               b.VenueId == venueId &&
                b.BookingDate == bookingDate);
        }
    }
}
