using HotelBookingAPI.Models;

namespace HotelBookingAPI.Tools
{
    public class BookingTools
    {
        public static Room GetDefaultRoom() => new() { Id = 1, Name = "Unique Room" };
    }
}
