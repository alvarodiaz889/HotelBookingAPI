using HotelBookingAPI.Models;
using HotelBookingAPI.ViewModels;

namespace HotelBookingAPI.Services
{
    public interface IBookingService
    {
        Task<BookingResponse<ICollection<BookingVM>>> GetAll();
        Task<BookingResponse<ICollection<BookingVM>>> GetByContactID(string id);
        Task<BookingResponse<BookingVM>> Create(CreateBookingVM booking);
        Task<BookingResponse<BookingVM>> Update(UpdateBookingVM booking);
        Task<BookingResponse<BookingVM>> Cancel(int id);
        Task<BookingResponse<ICollection<AvailabilityVM>>> GetAvailability();
    }
}
