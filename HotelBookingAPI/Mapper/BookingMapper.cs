using AutoMapper;
using HotelBookingAPI.Models;
using HotelBookingAPI.Tools;
using HotelBookingAPI.ViewModels;

namespace HotelBookingAPI.Mapper
{
    public class BookingMapper : Profile
    {
        public BookingMapper()
        {
            CreateMap<CreateBookingVM, Booking>()
                //.ForMember(dest => dest.BookingDate, opt => opt.BookingDate))
                .AfterMap((src, dest) =>
                {
                    dest.Contact = new Person 
                    { 
                        Id = src.ContactId,
                        Email = src.ContactEmail,
                        Age = src.ContactAge,
                        Name = src.ContactName,
                        Address = new Address
                        {
                            Country = src.Country,
                            State = src.State,
                            Street = src.Street,
                            ZipCode = src.ZipCode
                        }
                    };
                    dest.Room = BookingTools.GetDefaultRoom();
                    dest.BookingDate = DateTime.Now;
                })
                .ReverseMap();
        }
    }
}
