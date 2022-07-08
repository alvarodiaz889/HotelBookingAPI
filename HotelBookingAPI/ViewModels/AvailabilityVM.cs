namespace HotelBookingAPI.ViewModels
{
    public class AvailabilityVM
    {
        public IDictionary<int, ICollection<SlotVM>> Availability { get; set; }
    }
    public class SlotVM
    {
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
