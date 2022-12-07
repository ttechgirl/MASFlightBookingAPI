using MASFlightBookingAPI.Models;
using MASFlightBookingAPI.Repositories;
using MASFlightBookingAPI.View_Models;
using Microsoft.AspNetCore.Mvc;

namespace MASFlightBookingAPI.Controllers
{

    public class MASFlightController : ControllerBase
    {
        private readonly IMASFlightRepository _masFlightRepository;

        public MASFlightController(IMASFlightRepository masFlightRepository)
        {
            _masFlightRepository = masFlightRepository;
        }
        [HttpGet("checkFlightDetails")] //readonly i.e retrieiving list of all flight bookings from the database
        public IActionResult CheckMASFlight_Details()
        {
            var checkFlightDetails = _masFlightRepository.CheckMASFlight_Details();
            if (checkFlightDetails == null)
            {
                return NotFound();


            }
            return Ok(checkFlightDetails);

        }
        [HttpPost("buyFlightTicket")]

        //user will be able to create/book ticket and save changes
        public IActionResult BuyFlight_Ticket(MASFlightBookingModel masflight)
        {
            //if (!ModelState.IsValid) {return BadRequest(ModelState);}
            var buyFlightTicket = _masFlightRepository.BuyFlight_Ticket(masflight);
            if (buyFlightTicket.Success == true)
            {
                return Ok(buyFlightTicket);
            }
            return BadRequest();
        }
        [HttpDelete("{bookingId}")]
        public IActionResult Revoke_Flight(long BookingId)
        {
            var bookingId = _masFlightRepository.Revoke_Flight(BookingId);
            if (bookingId.Success == true)
            {
                return Ok(bookingId);

            }
            return NotFound();

        }
        [HttpPut("existBookingId")]
        public IActionResult UpdateFlight_Details(MASFlightBooking masflight)
        {
            var existBookingId = _masFlightRepository.UpdateFlight_Details(masflight);
            if (existBookingId.Success == true)
            {
                return Ok(existBookingId);
            }
            return BadRequest();
        }
    }







}
