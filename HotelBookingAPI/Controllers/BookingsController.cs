﻿using HotelBookingAPI.Services;
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
        [Route("Availability")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ICollection<AvailabilityVM>>> GetAvailability()
        {
            var result = await _bookingService.GetAvailability();
            return Ok(result);
        }

        [HttpPost]
        [Produces("application/json")]
        [Route("Bookings")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<int>> CreateBooking(CreateBookingVM booking)
        {
            var result = await _bookingService.GetAvailability();
            return Ok(result);
        }

    }
}
