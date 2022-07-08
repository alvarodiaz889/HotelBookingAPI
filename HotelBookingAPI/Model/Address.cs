using System.ComponentModel.DataAnnotations;

namespace HotelBookingAPI.Model
{
    public class Address
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Street { get; set; }

        public string ZipCode { get; set; }

        public string State { get; set; }
        public string Country { get; set; }

    }
}
