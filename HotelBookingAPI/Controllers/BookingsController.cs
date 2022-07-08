using HotelBookingAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingsController : Controller
    {
        private readonly ILogger<BookingsController> _logger;

        public BookingsController(ILogger<BookingsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Produces("application/json")]
        [Route("Availability")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AvailabilityVM>> GetAvailability()
        {
            var dic = new Dictionary<int, ICollection<SlotVM>>();
            dic.Add(1, 
                new List<SlotVM> { 
                    new SlotVM {
                        Name = "Room test",
                        Start = DateTime.Now,
                        End = DateTime.Now.AddDays(3)
                    }
                });
            return await Task.FromResult(new AvailabilityVM {
                Availability = dic
            });
        }

    }
}
