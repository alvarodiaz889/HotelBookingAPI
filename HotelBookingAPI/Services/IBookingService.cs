using HotelBookingAPI.Models;

namespace HotelBookingAPI.Services
{
    public interface IBookingService
    {
        Task<BookingResponse<ICollection<Booking>>> GetAll();
        Task<BookingResponse<ICollection<Booking>>> GetByContactID(string id);
        Task<BookingResponse<Booking>> Create(Booking booking);

        Task<BookingResponse<Booking>> Update(Booking booking);
        Task<BookingResponse<Booking>> Cancel(int id);
    }
}
