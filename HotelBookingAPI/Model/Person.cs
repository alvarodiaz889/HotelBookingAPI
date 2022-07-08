using System.ComponentModel.DataAnnotations;

namespace HotelBookingAPI.Model
{
    public class Person
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        public int Age { get; set; }

        public virtual Address Address { get; set; }

    }
}
