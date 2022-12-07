using Microsoft.AspNetCore.Identity;

namespace MASFlightBookingAPI.Models
{
    public class Users : IdentityUser
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }


    }
}
