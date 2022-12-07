using MASFlightBookingAPI.View_Models;
using System.Threading.Tasks;

namespace MASFlightBookingAPI.Repositories
{
    public interface IPaymentRepository
    {
        public interface IPaymentRepository
        {
            Task<PaymentResponseModel> InitiatePayment(PaymentRequestModel model);

            Task<PaymentVerificationResponseModel> VerifyPayment(string transactionId);

        }


    }
}
