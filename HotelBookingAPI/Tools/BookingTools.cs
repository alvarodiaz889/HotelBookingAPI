using HotelBookingAPI.Models;

namespace HotelBookingAPI.Tools
{
    public class BookingTools
    {
        public static Room GetDefaultRoom() => new() { Id = 1, Name = "Unique Room" };

        static char[] _chars = Enumerable.Range(48, 10)
                .Union(Enumerable.Range(65, 26))
                .Select(x => (char)x)
                .ToArray();
        public static string GenerateReservationCode(int len)
        {
            char[] code = new char[len];
            var rand = new Random();
            for (int i = 0; i < len; i++)
            {
                var idx = rand.Next(0, _chars.Length);
                code[i] = _chars[idx];
            }
            return new string(code);
        }
       
    }
}
