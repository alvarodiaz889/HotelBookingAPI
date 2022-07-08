using System.ComponentModel.DataAnnotations;

namespace HotelBookingAPI.ViewModels
{
    public class CreateBookingVM
    {
        [Required]
        public double TotalCost { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public string ContactId { get; set; }

        [Required]
        public string ContactName { get; set; }

        [Required]
        public string ContactEmail { get; set; }

        [Required]
        public int ContactAge { get; set; }

        [Required]
        public string Street { get; set; }

        public string ZipCode { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string Country { get; set; }
    }
}
