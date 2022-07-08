using System.ComponentModel.DataAnnotations;

namespace HotelBookingAPI.Models
{
    public class Address
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Street { get; set; }

        public string ZipCode { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string Country { get; set; }

    }
}
