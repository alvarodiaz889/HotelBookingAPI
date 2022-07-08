using System.ComponentModel.DataAnnotations;

namespace HotelBookingAPI.Models
{
    public class Room
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        static Room GetDefault() { return new Room { Id = 1, Name = "Unique Room" }; }
    }
}
