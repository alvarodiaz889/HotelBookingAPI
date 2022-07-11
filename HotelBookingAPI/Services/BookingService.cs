﻿using AutoMapper;
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

        public async Task<BookingResponse<Booking>> Create(CreateBookingVM booking)
        {
            var b = _mapper.Map<Booking>(booking);
            await _dbContext.Bookings.AddAsync(b);
            return new BookingResponse<Booking>(b, string.Empty, true);
        }
        public async Task<BookingResponse<Booking>> Update(UpdateBookingVM booking)
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

        public async Task<BookingResponse<ICollection<AvailabilityVM>>> GetAvailability()
        {
            var today = DateTime.Today;
            var startDay = today.AddDays(1).Date;
            var endDay = today.AddDays(31).Date;
            
            var booked = await _dbContext.Bookings
                .Where(w => w.Status == Booking.BookingStatus.Completed)
                .Where(w => w.StartDate >= startDay && w.StartDate <= endDay)
                .ToListAsync();

            var room = BookingTools.GetDefaultRoom();
            var availability = Enumerable.Range(1, 30)
                .Select(s => today.AddDays(s))
                .Where(w => booked.Any(b => w >= b.StartDate && w <= b.EndDate) == false)
                .ToList();

            var vm = new AvailabilityVM[] { new AvailabilityVM(room.Id, availability) };
            return new BookingResponse<ICollection<AvailabilityVM>>(vm, string.Empty, true);
        }
    }
}
