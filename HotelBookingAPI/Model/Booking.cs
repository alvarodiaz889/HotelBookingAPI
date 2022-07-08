using System.ComponentModel.DataAnnotations;

namespace HotelBookingAPI.Model
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ReservationCode { get; set; }
        
        public Person Contact { get; set; }

        [Required]
        public double TotalCost { get; set; }

        [Required]
        public DateTime BookingDate { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public BookingStatus Status { get; set; }
    }
}
