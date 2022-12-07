using MASFlightBookingAPI.Models;
using System;

namespace MASFlightBookingAPI.View_Models
{
    public class MASFlightBookingModel
    {
        public long Id { get; set; }
        public long BookingId { get; set; }
        public string TicketName { get; set; }
        public int Number_of_passanger { get; set; }
        public Airline airline { get; set; }
        public Departure departure { get; set; }
        public Destination destination { get; set; }
        public DateTime dateTime { get; set; } = DateTime.Now;
        public FlightCategories flightCategories { get; set; }
        public TravelerAge travelerAge { get; set; }
        public Trip_Type Trip_Type { get; set; }
        public bool IsDeleted { get; set; } = false;
        public decimal amount { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string phonenumber { get; set; }


    }
}
