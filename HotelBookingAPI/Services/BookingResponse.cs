namespace HotelBookingAPI.Services
{
    public class BookingResponse<T>
    {
        public BookingResponse(T data, string message, bool success)
        {
            Data = data;
            Message = message;
            Success = success;
        }

        public T Data { get; private set; }
        public string Message { get; private set; }
        public bool Success { get; private set; }
    }
}
