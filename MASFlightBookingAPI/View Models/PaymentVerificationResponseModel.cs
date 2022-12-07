namespace MASFlightBookingAPI.View_Models
{
    public class PaymentVerificationResponseModel
    {
        public string message { get; set; }
        public string status { get; set; }
        public FlutterWaveData data { get; set; }

    }
    public class FlwData
    {
        public string id { get; set; }
        public string account_id { get; set; }
        public string tx_id { get; set; }
        public string flw_ref { get; set; }
        public string wallet_id { get; set; }
        public decimal amount_refunded { get; set; }
        public string status { get; set; }
        public string destination { get; set; }

    }


}
