using System.ComponentModel.DataAnnotations;
using static HotelBookingAPI.Models.Booking;

namespace HotelBookingAPI.ViewModels
{
    public class BookingVM
    {
        public int Id { get; set; }

        public string ReservationCode { get; set; }

        public string ContactId { get; set; }
        
        public string ContactName { get; set; }

        public string ContactEmail { get; set; }

        public double TotalCost { get; set; }

        public DateTime BookingDate { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string Status { get; set; }

        public string RoomId { get; set; }
        public string RoomName { get; set; }
    }
}
