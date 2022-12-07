﻿using System.ComponentModel.DataAnnotations;

namespace MASFlightBookingAPI.View_Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }

    }
}
