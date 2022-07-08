using System.ComponentModel.DataAnnotations;

namespace HotelBookingAPI.Models
{
    public class Booking
    {
        public enum BookingStatus
        {
            Completed,
            Cancelled
        }

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

        public DateTime ModifiedDate { get; set; }

        [Required]
        public BookingStatus Status { get; set; }

        [Required]
        public Room Room { get; set; } 
    }
}
