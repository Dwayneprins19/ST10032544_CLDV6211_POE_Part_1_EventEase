using System.ComponentModel.DataAnnotations;    

namespace EventEase.Models
{
    public class Event
    {
        public int EventId { get; set; }

        [Required]
        public string EventName { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ImageUrl { get; set; }
    }
}
