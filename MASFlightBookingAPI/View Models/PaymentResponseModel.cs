namespace MASFlightBookingAPI.View_Models
{
    public class PaymentResponseModel
    {
        public string message { get; set; }
        public string status { get; set; }
        public FlutterWaveData data { get; set; }

    }
    public class FlutterWaveData
    {
        public string link { get; set; }
    }

}
