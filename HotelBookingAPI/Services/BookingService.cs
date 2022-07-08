using HotelBookingAPI.Data;
using HotelBookingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingAPI.Services
{
    public class BookingService : IBookingService
    {
        private readonly ApiDBContext _dbContext;
        public BookingService(ApiDBContext context)
        {
            _dbContext = context;
        }
        public async Task<BookingResponse<ICollection<Booking>>> GetAll()
        {
            var bookings = await _dbContext.Bookings.ToListAsync();
            return new BookingResponse<ICollection<Booking>>(bookings, string.Empty, true);
        }

        public async Task<BookingResponse<ICollection<Booking>>> GetByContactID(string id)
        {
            var bookings = await _dbContext.Bookings.Where(w => w.Contact.Id == id).ToListAsync();
            return new BookingResponse<ICollection<Booking>>(bookings, string.Empty, true);
        }

        public async Task<BookingResponse<Booking>> Update(Booking booking)
        {
            var b = await _dbContext.Bookings
                .FirstOrDefaultAsync(b => b.Id == booking.Id 
                    && b.ReservationCode == booking.ReservationCode );
            if (b == null)
                return new BookingResponse<Booking>(null, "Booking doesn't exist", false);

            b.StartDate = booking.StartDate;
            b.EndDate = booking.EndDate;
            b.ModifiedDate = DateTime.Now;

            _dbContext.Bookings.Update(b);
            await _dbContext.SaveChangesAsync();

            return new BookingResponse<Booking>(b, string.Empty, true);
        }

        public async Task<BookingResponse<Booking>> Cancel(int id)
        {
            var b = await _dbContext.Bookings.FirstOrDefaultAsync(b => b.Id == id);
            if (b == null)
                return new BookingResponse<Booking>(null, "Booking doesn't exist", false);

            b.Status = Booking.BookingStatus.Cancelled;
            b.ModifiedDate = DateTime.Now;

            _dbContext.Bookings.Update(b);
            await _dbContext.SaveChangesAsync();

            return new BookingResponse<Booking>(b, string.Empty, true);
        }

        public async Task<BookingResponse<Booking>> Create(Booking booking)
        {
            await _dbContext.Bookings.AddAsync(booking);
            return new BookingResponse<Booking>(booking, string.Empty, true);
        }
    }
}
