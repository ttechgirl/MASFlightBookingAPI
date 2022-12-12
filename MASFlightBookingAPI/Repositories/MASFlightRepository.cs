using MASFlightBookingAPI.Models;
using MASFlightBookingAPI.View_Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MASFlightBookingAPI.Repositories
{
    public class MASFlightRepository : IMASFlightRepository
    {

        private MASFlightDbContext _masFlightDbContext;
        private readonly IPaymentRepository _paymentRepository;

        public MASFlightRepository(MASFlightDbContext masFlightDbContext, IPaymentRepository paymentRepository)
        {
            _masFlightDbContext = masFlightDbContext;
            _paymentRepository =  paymentRepository;
        }
        public ResponseModel Revoke_Flight(long BookingId)
        {
            var response = new ResponseModel();
            var bookingId = CheckMASFlight_Details(BookingId);
            if (bookingId != null)
            {

                _masFlightDbContext.Remove(bookingId);
                _masFlightDbContext.SaveChanges();

                response.Success = true;
                response.Error = "Ticket successfully deleted";

            }
            else
            {
                response.Success = false;
                response.Error = "Ticket Id cannot be found";
            }
            return response;
        }

          public List<MASFlightBooking> GetMASFlights()
        {
            var ticketExist = _masFlightDbContext.Set<MASFlightBooking>().ToList();
            return ticketExist;
        }

        public MASFlightBooking CheckMASFlight_Details(long BookingId)
        {

            var ticketExist = _masFlightDbContext.Set<MASFlightBooking>().FirstOrDefault(t => t.BookingId == BookingId);


            return ticketExist;
        }

      

        public ResponseModel BuyFlight_Ticket(MASFlightBookingModel masflight)
        {

            var response = new ResponseModel();

            var existUser = _masFlightDbContext.Set<MASFlightBooking>().Where(t => t.TicketName == masflight.TicketName &&
                                                                              t.BookingId == masflight.BookingId).ToList();

            if (existUser.Count > 0)
            {
                response.Success = false;
                response.Error = "User with the same Ticket details exist";
            }
            else
            {
                var rand = new Random();
                int tranId = rand.Next(1000);
                var tx_ref = $"Flight-{tranId}-{DateTime.Now}";

                var sendPaymentData = new PaymentRequestModel()
                {
                    tx_ref = tx_ref,
                    amount = masflight.amount,
                    currency = "NGN",
                    redirect_url = "https://localhost:5501",
                    payment_options = "card",
                    customer = new Customer()
                    {
                        email = masflight.email,
                        name = masflight.name,
                        phonenumber = masflight.phonenumber

                    },


                };
                var request = _paymentRepository.InitiatePayment(sendPaymentData).Result;


                var buyTicket = new MASFlightBooking
                {
                    TicketName = masflight.TicketName,
                    Number_of_passanger = masflight.Number_of_passanger,
                    destination = masflight.destination,
                    departure = masflight.departure,
                    Trip_Type = masflight.Trip_Type,
                    flightCategories = masflight.flightCategories,
                    travelerAge = masflight.travelerAge,
                    airline = masflight.airline,

                };

                _masFlightDbContext.Add(buyTicket);
                _masFlightDbContext.SaveChanges();

                response.Success = true;
                response.Error = request.data.link;


            }

            return response;
        }


        public ResponseModel UpdateFlight_Details(MASFlightBooking masflight)
        {
            var response = new ResponseModel();
            var existUser = _masFlightDbContext.Set<MASFlightBooking>().Where(t => t.BookingId == masflight.BookingId).ToList();
            //if userId exist flight details will be updated 
            if (existUser != null)
            {
                _masFlightDbContext.Update(masflight);
                _masFlightDbContext.SaveChanges();

                response.Success = true;
                response.Error = "Flight details updated";

            }
            else
            {
                response.Success = false;
                response.Error = "User not found ,kindly create a flight details";
            }
            return response;

        }
    }


}

