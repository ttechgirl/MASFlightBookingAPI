namespace MASFlightBookingAPI.View_Models
{
    public class PaymentRequestModel
    {
        public long Id { get; set; }
        public string tx_ref { get; set; }
        public decimal amount { get; set; }
        public string currency { get; set; } = "NGN";
        public string redirect_url { get; set; }
        public string payment_options { get; set; } = "card";
        public Customer customer { get; set; }

        // public Customization customizations { get; set;} (optional)
    }

    public class Customer
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string phonenumber { get; set; }
    }

    //optional
    /*public class Customization
     {
         public string title { get; set; }
         public string description { get; set; }
         public string logo { get; set; }=""
     }*/

}
