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

        [HttpGet("Get_Available_Flights")]
        public IActionResult GetMASFlights()
        {
            var availableFlights = _masFlightRepository.GetMASFlights();
            if (availableFlights == null)
            {
                return NotFound();


            }
            return Ok(availableFlights);

        }
       

        [HttpGet("Check_Flight_Details")] //readonly i.e retrieiving list of all flight bookings from the database
        public IActionResult CheckMASFlight_Details(long BookingId)
        {
            var checkFlightDetails = _masFlightRepository.CheckMASFlight_Details(BookingId);
            return Ok(checkFlightDetails);
        }


        [HttpPost("Buy_Flight_Ticket")]

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

        [HttpDelete("{Revoke_Flight}")]
        public IActionResult Revoke_Flight(long BookingId)
        {
            var bookingId = _masFlightRepository.Revoke_Flight(BookingId);
            if (bookingId.Success == true)
            {
                return Ok(bookingId);

            }
            return NotFound();

        }

        [HttpPut("Update_Flight_Details")]
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
