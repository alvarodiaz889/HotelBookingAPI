using System.ComponentModel.DataAnnotations;

namespace HotelBookingAPI.ViewModels
{
    public class CancelBookingVM
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string ReservationCode { get; set; }

    }
}
