﻿using System.ComponentModel.DataAnnotations;

namespace nPrimeApi.Models.Accounts
{
    public class RegisterNewUser
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}