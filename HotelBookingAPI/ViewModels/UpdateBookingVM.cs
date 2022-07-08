using System.ComponentModel.DataAnnotations;

namespace HotelBookingAPI.ViewModels
{
    public class UpdateBookingVM
    {
        [Required]
        public int Id { get; set; }
        
        [Required]
        public string ReservationCode { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }
    }
}
