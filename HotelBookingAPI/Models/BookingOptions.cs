namespace HotelBookingAPI.Models
{
    public class BookingOptions
    {
		public const string SECTION = "BookingOptions";

        public int MaxBookingDaysAllowance { get; set; }
        public int MaxAdvanceDaysToBook { get; set; }
        public int MinStartDaysToBook { get; set; }
    }
}
