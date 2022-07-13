using AutoMapper;
using HotelBookingAPI.Data;
using HotelBookingAPI.Models;
using HotelBookingAPI.Tools;
using HotelBookingAPI.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace HotelBookingAPI.Services
{
    public class BookingService : IBookingService
    {
        private readonly ApiDBContext _dbContext;
        private readonly BookingOptions _options;
        private readonly IMapper _mapper;
        public BookingService(ApiDBContext context,
                    IOptions<BookingOptions> options,
                    IMapper mapper)
        {
            _dbContext = context;
            _options = options.Value;
            _mapper = mapper;
        }
        public async Task<BookingResponse<ICollection<BookingVM>>> GetAll()
        {
            var bookings = await _dbContext.Bookings
                .Include(i => i.Contact)
                .Include(i => i.Room)
                .ToListAsync();

            var mapped = _mapper.Map<ICollection<BookingVM>>(bookings);
            return new BookingResponse<ICollection<BookingVM>>(mapped, string.Empty, true);
        }

        public async Task<BookingResponse<ICollection<BookingVM>>> GetByContactID(string id)
        {
            var bookings = await _dbContext.Bookings
                .Where(w => w.Contact.Id == id)
                .ToListAsync();
            var mapped = _mapper.Map<ICollection<BookingVM>>(bookings);
            return new BookingResponse<ICollection<BookingVM>>(mapped, string.Empty, true);
        }

        public async Task<BookingResponse<BookingVM>> Create(CreateBookingVM booking)
        {
            var b = _mapper.Map<CreateBookingVM, Booking>(booking);

            if (!await IsDateRangeValid(b.StartDate, b.EndDate, null))
                return new BookingResponse<BookingVM>(null, "Date range is not valid", false);

            var code = BookingTools.GenerateReservationCode(6);
            while (_dbContext.Bookings.FirstOrDefault(f => f.ReservationCode == code) != null)
                code = BookingTools.GenerateReservationCode(6);
            b.ReservationCode = code;

            b.Room = await _dbContext.Rooms.FirstOrDefaultAsync(f => f.Id == BookingTools.GetDefaultRoom().Id);

            var contact = await _dbContext.People.FirstOrDefaultAsync(f => f.Id == b.Contact.Id);
            if (contact != null)
                b.Contact = contact;

            await _dbContext.Bookings.AddAsync(b);
            await _dbContext.SaveChangesAsync();

            var mapped = _mapper.Map<BookingVM>(b);
            return new BookingResponse<BookingVM>(mapped, string.Empty, true);
        }
        public async Task<BookingResponse<BookingVM>> Update(UpdateBookingVM booking)
        {
            var b = await _dbContext.Bookings
                .Include(i => i.Contact)
                .Include(i => i.Room)
                .FirstOrDefaultAsync(b => b.Id == booking.Id
                    && b.ReservationCode == booking.ReservationCode);
            if (b == null)
                return new BookingResponse<BookingVM>(null, "Booking doesn't exist", false);

            if (!await IsDateRangeValid(booking.StartDate, booking.EndDate, b))
                return new BookingResponse<BookingVM>(null, "Date range is not valid", false);

            b.StartDate = booking.StartDate.Date;
            b.EndDate = booking.EndDate.Date;
            b.ModifiedDate = DateTime.Now;

            _dbContext.Bookings.Update(b);
            await _dbContext.SaveChangesAsync();

            var mapped = _mapper.Map<BookingVM>(b);
            return new BookingResponse<BookingVM>(mapped, string.Empty, true);
        }

        public async Task<BookingResponse<BookingVM>> Cancel(CancelBookingVM booking)
        {
            var b = await _dbContext.Bookings
                .Include(i => i.Contact)
                .Include(i => i.Room)
                .FirstOrDefaultAsync(b => b.Id == booking.Id
                    && b.ReservationCode == booking.ReservationCode);

            if (b == null)
                return new BookingResponse<BookingVM>(null, "Booking doesn't exist", false);

            b.Status = Booking.BookingStatus.Cancelled;
            b.ModifiedDate = DateTime.Now;

            _dbContext.Bookings.Update(b);
            await _dbContext.SaveChangesAsync();

            var mapped = _mapper.Map<BookingVM>(b);
            return new BookingResponse<BookingVM>(mapped, string.Empty, true);
        }        

        public async Task<BookingResponse<ICollection<AvailabilityVM>>> GetAvailability()
        {
            var today = DateTime.Today;
            var startDay = today.AddDays(_options.MinStartDaysToBook).Date;
            var endDay = today.AddDays(_options.MaxAdvanceDaysToBook).Date;
            
            var booked = await _dbContext.Bookings
                .Where(w => w.Status == Booking.BookingStatus.Completed)
                .Where(w => w.StartDate >= startDay && w.StartDate <= endDay)
                .ToListAsync();

            var room = BookingTools.GetDefaultRoom();
            var availability = Enumerable.Range(_options.MinStartDaysToBook, _options.MaxAdvanceDaysToBook - _options.MinStartDaysToBook)
                .Select(s => today.AddDays(s))
                .Where(w => booked.Any(b => w >= b.StartDate && w <= b.EndDate) == false)
                .ToList();

            var vm = new AvailabilityVM[] { new AvailabilityVM(room.Id, availability) };
            return new BookingResponse<ICollection<AvailabilityVM>>(vm, string.Empty, true);
        }

        private async Task<bool> IsDateRangeValid(DateTime start, DateTime end, Booking booking)
        {
            if ((end - start).TotalDays > _options.MaxBookingDaysAllowance) 
                return false;

            var today = DateTime.Today;
            if (start < today.AddDays(_options.MinStartDaysToBook))
                return false;

            var query = _dbContext.Bookings
                .Where(w => w.Status == Booking.BookingStatus.Completed)
                .Where(w => (start.Date >= w.StartDate && start.Date <= w.EndDate)
                        || (end.Date >= w.StartDate && end.Date <= w.EndDate));

            if (booking != null)
                query = query.Where(w => w.Id != booking.Id);

            var booked = await query.CountAsync();

            return booked <= 0;
        }
    }
}
