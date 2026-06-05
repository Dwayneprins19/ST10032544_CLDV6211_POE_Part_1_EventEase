using System.ComponentModel.DataAnnotations;
using System.Security.Permissions;

namespace EventEase.Models
{
    public class Event
    {
        public int EventId { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? ImageUrl { get; set; }
        public int EventTypeId { get; set; }
        public EventType? EventType { get; set; }
        public ICollection<Booking>? Bookings { get; set; }
    }
}
