using HotelBookingAPI.Services;
using HotelBookingAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingsController : Controller
    {
        private readonly ILogger<BookingsController> _logger;
        private readonly IBookingService _bookingService;
        public BookingsController(ILogger<BookingsController> logger, IBookingService service)
        {
            _logger = logger;
            _bookingService = service;
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ICollection<BookingVM>>> GetBookings()
        {
            var result = await _bookingService.GetAll();
            return Ok(result);
        }

        [HttpGet]
        [Produces("application/json")]
        [Route("Availability")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ICollection<AvailabilityVM>>> GetAvailability()
        {
            var result = await _bookingService.GetAvailability();
            return Ok(result);
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BookingResponse<BookingVM>>> CreateBooking(CreateBookingVM booking)
        {
            var result = await _bookingService.Create(booking);
            return Ok(result);
        }

        [HttpPut]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BookingResponse<BookingVM>>> UpdateBooking(UpdateBookingVM booking)
        {
            var result = await _bookingService.Update(booking);
            return Ok(result);
        }

        [HttpDelete]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BookingResponse<BookingVM>>> CancelBooking(CancelBookingVM booking)
        {
            var result = await _bookingService.Cancel(booking);
            return Ok(result);
        }

    }
}
