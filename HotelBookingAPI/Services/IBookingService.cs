using HotelBookingAPI.Models;
using HotelBookingAPI.ViewModels;

namespace HotelBookingAPI.Services
{
    public interface IBookingService
    {
        Task<BookingResponse<ICollection<Booking>>> GetAll();
        Task<BookingResponse<ICollection<Booking>>> GetByContactID(string id);
        Task<BookingResponse<Booking>> Create(CreateBookingVM booking);
        Task<BookingResponse<Booking>> Update(UpdateBookingVM booking);
        Task<BookingResponse<Booking>> Cancel(int id);
        Task<BookingResponse<ICollection<AvailabilityVM>>> GetAvailability();
    }
}
