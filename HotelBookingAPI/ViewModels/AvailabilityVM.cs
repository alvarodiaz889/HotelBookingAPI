namespace HotelBookingAPI.ViewModels
{
    public class AvailabilityVM
    {
        public AvailabilityVM(int roomId, ICollection<DateTime> availability)
        {
            RoomId = roomId;
            Availability = availability;
        }
        public int RoomId { get; set; }
        public ICollection<DateTime> Availability { get; set; }
    }
}
