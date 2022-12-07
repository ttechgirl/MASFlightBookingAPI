using System;

namespace MASFlightBookingAPI.Models
{
    public class MASFlightBooking
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


    }



    public enum Trip_Type
    {
        RoundTrip,
        OneWay
    }

    public enum FlightCategories
    {
        Economy,
        Premium,
        FirstClass,
        Business
    }
    public enum TravelerAge
    {
        Adult,
        Children,
        Infant
    }

    public enum Departure
    {
        Lagos_LOS,
        Abuja_ABV,
        Portharcourt_PHC,
        Anambra_ANA,
        Enugu_ENU,
        Ilorin_ILR,
        Kaduna_KAD,
        Kano_KAN,
        Sokoto_SKO

    }
    public enum Airline
    {
        Dana_Air,
        IRS,
        Ibom_Air,
        Mas_Air,
        Air_France
    }
    public enum Destination
    {
        Lagos_LOS,
        Abuja_ABV,
        Portharcourt_PHC,
        Anambra_ANA,
        Enugu_ENU,
        Ilorin_ILR,
        Kaduna_KAD,
        Kano_KAN,
        Sokoto_SKO
    }

}
