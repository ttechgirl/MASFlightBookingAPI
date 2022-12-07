using MASFlightBookingAPI.Models;
using System.Collections.Generic;
using MASFlightBookingAPI.View_Models;

namespace MASFlightBookingAPI.Repositories
{
    public interface IMASFlightRepository
    {

        List<MASFlightBooking> CheckMASFlight_Details();
        MASFlightBooking GetMASFlights(long BookingId);
        ResponseModel BuyFlight_Ticket(MASFlightBookingModel masflight);

        ResponseModel Revoke_Flight(long BookingId);

        ResponseModel UpdateFlight_Details(MASFlightBooking masflight);

    }
}
